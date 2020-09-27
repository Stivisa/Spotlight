using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public class Listing : Post
    {
        [Key]
        public int id { get; set; }
        public double DonationQuote { get; set; } = 0;
        public double CurrentDonationValue { get; set; } = 0;
        public string UserID { get; set; }
    }
}
