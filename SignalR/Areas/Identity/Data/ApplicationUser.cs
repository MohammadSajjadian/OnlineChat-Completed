using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SignalR.Models;

namespace SignalR.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string nameFamily { get; set; }

        public ICollection<Group> groups { get; set; }
        public ICollection<Message> messages { get; set; }
    }
}
