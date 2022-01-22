using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalR.Areas.Identity.Data;
using SignalR.Chat;
using SignalR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSignalR();

            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", y => y.RequireRole("admin"));
            });

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = "/Account/Login";
                x.AccessDeniedPath = "/Account/Login";
            });

            services.AddDbContext<DBsignalR>(x =>
            {
                x.UseSqlServer(Configuration.GetConnectionString("DBsignalR"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");
            });

            Init(userManager, roleManager).Wait();
        }

        private async Task Init(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            List<string> roles = new() { "admin", "user" };

            foreach (var item in roles)
            {
                var role = new IdentityRole(item);
                await roleManager.CreateAsync(role);
            }

            ApplicationUser user = await userManager.FindByNameAsync("admin");
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "admin",
                    nameFamily = "admin",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "pP_0987");
            }

            if (await userManager.IsInRoleAsync(user, "admin") == false)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}
