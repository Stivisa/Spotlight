using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.ViewModels
{
    public class NewsPostViewModel
    {
        public IEnumerable<NewsPost> NewsPosts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
