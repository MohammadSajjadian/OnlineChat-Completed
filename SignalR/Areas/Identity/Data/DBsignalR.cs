using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalR.Areas.Identity.Data;
using SignalR.Models;

namespace SignalR.Data
{
    public class DBsignalR : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Group> groups { get; set; }
        public DbSet<Message> messages { get; set; }

        public DBsignalR(DbContextOptions<DBsignalR> options)
            : base(options)
        {
        }
    }
}
