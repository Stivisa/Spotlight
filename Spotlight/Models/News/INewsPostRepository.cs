using System.Collections.Generic;

namespace Spotlight.Models
{
    public interface INewsPostRepository
    {
        IEnumerable<NewsPost> AllNewsPosts { get; }
        public abstract void AddNews(NewsPost n);
        public abstract void UpdateNewsPost(NewsPost n);
    }
}