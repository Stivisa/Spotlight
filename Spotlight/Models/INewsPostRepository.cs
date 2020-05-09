using System.Collections.Generic;

namespace Spotlight.Models
{
    public interface INewsPostRepository
    {
        IEnumerable<NewsPost> AllNewsPosts { get; }
    }
}