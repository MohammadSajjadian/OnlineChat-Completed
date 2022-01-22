using SignalR.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.Models
{
    public class Message
    {
        public int id { get; set; }

        public string text { get; set; }
        public DateTime time { get; set; }

        public int groupId { get; set; }
        [ForeignKey(nameof(groupId))]
        public Group group { get; set; }

        public string userId { get; set; }
        [ForeignKey(nameof(userId))]
        public ApplicationUser applicationUser { get; set; }
    }
}
