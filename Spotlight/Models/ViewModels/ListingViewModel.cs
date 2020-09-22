using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.ViewModels
{
    public class ListingViewModel
    {
        public IEnumerable<Listing> Listings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
