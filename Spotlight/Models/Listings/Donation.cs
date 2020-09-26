using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Listings
{
    public class Donation
    {
        public int value { get; set; }
        public Listing listing { get; set; }
    }
}
