using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Listings
{
    public class ListingDbContext : DbContext
    {
        public ListingDbContext(DbContextOptions<ListingDbContext> options) : base(options) { }

        public DbSet<Listing> AllListings { get; set; }
    }
}
