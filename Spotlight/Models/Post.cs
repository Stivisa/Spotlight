using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public class Post
    {
        public int id { get; set; }
        public string PostHeadline { get; set; }
        public string PostContent { get; set; }
        public DateTime? TimeOfPosting { get; set; } = null;
    }
}
