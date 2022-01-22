using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.Areas.Identity.Data;
using SignalR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Controllers
{
    public class ChatController : Controller
    {
        private readonly DBsignalR db;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatController(DBsignalR _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }


        [Authorize(Policy = "AdminPolicy")]
        public IActionResult ChatManagement()
        {
            db.groups.Include(x => x.applicationUser).Include(x => x.messages).ToList();

            return View(db.groups.ToList());
        }


        [Authorize]
        public async Task<IActionResult> Support()
        {
            ApplicationUser user = await userManager.FindByNameAsync(User.Identity.Name);

            db.messages.Include(x => x.group).Include(x => x.applicationUser).ToList();
            
            return View(db.messages.Where(x => x.group.userId == user.Id).OrderBy(x => x.time).ToList());
        }


        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminSideChat(string userId)
        {
            db.messages.Include(x => x.group).Include(x => x.applicationUser).ToList();
            
            ViewBag.userId = userId;

            return View(db.groups.FirstOrDefault(x => x.userId == userId).messages.OrderBy(x => x.time).ToList());
        }
    }
}
