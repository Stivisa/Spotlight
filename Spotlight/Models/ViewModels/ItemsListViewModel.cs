using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotlight.Models.Event;

namespace Spotlight.Models.ViewModels
{
    public class ItemsListViewModel
    {
        public IEnumerable<Models.Event.Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
