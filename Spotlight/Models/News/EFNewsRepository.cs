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

    public void UpdateNewsPost(NewsPost n)
        {
            var post = AllNewsPosts.FirstOrDefault(post => post.id == n.id);

            if (post == null)
                return;

            post.PostHeadline = n.PostHeadline;
            post.PostContent = n.PostContent;
            context.SaveChanges();
        }

        public void DeleteNewsPost(NewsPost n)
        {
            var post = AllNewsPosts.FirstOrDefault(post => post.id == n.id);

            if (post == null)
                return;

            context.Remove(post);
            context.SaveChanges();
        }
    }
}
