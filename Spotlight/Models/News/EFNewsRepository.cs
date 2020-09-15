using Spotlight.Models;
using Spotlight.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.News
{
    public class EFNewsRepository : INewsPostRepository
    {
        private NewsDbContext context;
        public EFNewsRepository(NewsDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<NewsPost> AllNewsPosts => context.AllNewsPosts;

        public void AddNews(NewsPost n)
        {
            context.Add(n);
            context.SaveChanges();
        }
    }
}
