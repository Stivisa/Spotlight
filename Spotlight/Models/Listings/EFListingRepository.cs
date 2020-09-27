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

        public void UpdateListing(Listing l)
        {
            var listing = AllListings.FirstOrDefault(post => post.id == l.id);

            if (listing == null)
                return;

            listing.PostHeadline = l.PostHeadline;
            listing.PostContent = l.PostContent;
            context.SaveChanges();
        }

        public void DeleteListing(Listing l)
        {
            var post = AllListings.FirstOrDefault(post => post.id == l.id);

            if (post == null)
                return;

            context.Remove(post);
            context.SaveChanges();
        }
    }
}
