using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SignalR.Areas.Identity.Data;
using SignalR.Data;
using SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Chat
{
    public class ChatHub : Hub
    {
        private readonly DBsignalR db;
        private readonly UserManager<ApplicationUser> userManager;

        public ChatHub(DBsignalR _db, UserManager<ApplicationUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }


        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }


        public async Task SendMessageToGroup(string clientId, string currentUser, string message)
        {
            ApplicationUser client = await userManager.FindByIdAsync(clientId);
            ApplicationUser CurrentUser = await userManager.FindByIdAsync(currentUser);

            if (db.groups.Any(x => x.userId == client.Id) == false)
            {
                Group mainGroup = new() { userId = client.Id };
                
                db.Add(mainGroup);
                db.SaveChanges();
                
                Message mainMessage = new()
                {
                    text = message,
                    time = DateTime.Now,
                    groupId = mainGroup.id,
                    userId = CurrentUser.Id
                };

                db.Add(mainMessage);
                db.SaveChanges();
            }
            else
            {
                Group mainGroup = db.groups.FirstOrDefault(x => x.userId == client.Id);

                Message mainMessage = new()
                {
                    text = message,
                    time = DateTime.Now,
                    groupId = mainGroup.id,
                    userId = CurrentUser.Id
                };

                db.Add(mainMessage);

                db.SaveChanges();
            }

            await Clients.Group(clientId).SendAsync("ReceiveMessage", CurrentUser.Id, CurrentUser.nameFamily, message);
        }
    }
}
