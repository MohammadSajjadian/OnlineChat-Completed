using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignalR.Areas.Identity.Data;
using SignalR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }


        #region Register

        public IActionResult Register()
        {
            return View();
        }


        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.userName,
                nameFamily = model.nameFamily,
                EmailConfirmed = true
            };

            var status = await userManager.CreateAsync(user, model.password);
            if (status.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "user");

                await signInManager.PasswordSignInAsync(user, model.password, true, false);

                TempData["msg"] = "ثبت نام با موفقیت انجام شد";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["msg"] = "ثبت نام با شکست مواجه شد";

                return RedirectToAction(nameof(Register));
            }
        }


        public async Task<IActionResult> IsUserExist(string userName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);

            if (user != null)
                return Json(false);
            else
                return Json(true);
        }

        #endregion


        #region Login

        public IActionResult Login()
        {
            return View();
        }


        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.userName);
            if (user != null)
            {
                var status = await signInManager.PasswordSignInAsync(user, model.password, true, false);
                if (status.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["msg"] = "نام کاربری یا رمز عبور اشتباه است";

                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["msg"] = "کاربر موجود نیست";

                return RedirectToAction(nameof(Login));
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("index", "Home");
        }

        #endregion
    }
}
