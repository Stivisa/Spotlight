using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public interface IListingRepository
    {
        IEnumerable<Listing> AllListings { get; }
        public void AddListing(Listing l);
    }
}
