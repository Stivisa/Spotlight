using System;
using System.Collections.Generic;

namespace Spotlight.Models
{
    public class MockNewsRepository : INewsPostRepository
    {
        public IEnumerable<NewsPost> AllNewsPosts => new List<NewsPost> {
            new NewsPost { PostHeadline = "This is 1st piece of news", PostContent = "Cogito ergo sum.", TimeOfPosting = new DateTime(2020, 6, 1, 12, 32, 30) },
            new NewsPost { PostHeadline = "This is 3rd piece of news", PostContent = "Per aspera ad astra.", TimeOfPosting = new DateTime(2013, 11, 11, 12, 32, 30) },
            new NewsPost { PostHeadline = "This is 2nd piece of news", PostContent = "Consuetudinis magna vis est.", TimeOfPosting = new DateTime(2015, 2, 2, 12, 32, 30) }
        };
    }
}