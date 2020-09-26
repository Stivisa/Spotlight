using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Listings
{
    public class EFListingRepository : IListingRepository
    {
        private ListingDbContext context;
        public EFListingRepository(ListingDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Listing> AllListings => context.AllListings;

        public void AddListing(Listing l)
        {
            context.Add(l);
            context.SaveChanges();
        }

        public void AddDonation(Donation d)
        {
            var l = AllListings.FirstOrDefault(l => l.id == d.listing.id);

            if (l == null)
                return;

            l.CurrentDonationValue += d.value;
            context.SaveChanges();
        }
    }
}
