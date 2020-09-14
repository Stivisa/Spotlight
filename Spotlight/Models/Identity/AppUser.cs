using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Spotlight.Models.Identity
{
    public class AppUser : IdentityUser
    {
        //additional members for basic Identity installation
        // 1 - * AppUser || Message
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
