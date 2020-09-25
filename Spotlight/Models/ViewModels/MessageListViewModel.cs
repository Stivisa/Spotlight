using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotlight.Models.Identity;

namespace Spotlight.Models.ViewModels
{
    public class MessageListViewModel
    {
        public IEnumerable<Models.Identity.Message> Messages { get; set; }
        public string messageText { get; set; }
    }
}
