using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public class MockListRepository : IListingRepository
    {
        private static List<Listing> allListings = new List<Listing> {
                new Listing { PostHeadline = "This is 1st listing", PostContent = "Cogito ergo sum.", TimeOfPosting = new DateTime(2020, 6, 1, 12, 32, 30) },
                new Listing { PostHeadline = "This is 3rd listing", PostContent = "Per aspera ad astra.", TimeOfPosting = new DateTime(2013, 11, 11, 12, 32, 30) },
                new Listing { PostHeadline = "This is 2nd listing", PostContent = "Consuetudinis magna vis est.", TimeOfPosting = new DateTime(2015, 2, 2, 12, 32, 30) }
        };

        public void AddListing(Listing l) => allListings.Add(l);

        public IEnumerable<Listing> AllListings => allListings;
    }
}
