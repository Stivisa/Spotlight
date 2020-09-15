using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Spotlight.Models.News
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options) { }
        public DbSet<NewsPost> AllNewsPosts { get; set; }

    }
}
