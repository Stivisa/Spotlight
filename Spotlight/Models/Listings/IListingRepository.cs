using Spotlight.Models.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public interface IListingRepository
    {
        IEnumerable<Listing> AllListings { get; }
        IEnumerable<DonationRecord> DonationRecords { get; }
        public void AddListing(Listing l);
        public void AddDonation(Donation d);
        public void UpdateListing(Listing l);
        public void DeleteListing(Listing l);
        public void AddDonationRecord(int value);
    }

}
