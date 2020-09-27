using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Spotlight.Models;

namespace Spotlight.Models.Identity
{
    public class AppUser : IdentityUser
    {
        //additional members for basic Identity installation
        // 1 - * AppUser || Message
        public AppUser()
        {
            Messages = new HashSet<Message>();
            Events = new HashSet<Event.Event>();
        }
        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Event.Event> Events { get; set; }

        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
