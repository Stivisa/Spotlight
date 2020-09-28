using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Listings
{
    public class DonationRecord
    {
        public int Id { get; set; }

        public int value { get; set; }      

        public DateTime When { get; set; }       

        public DonationRecord()
        {
            When = DateTime.Now;
        }
    }
}
