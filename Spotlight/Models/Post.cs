using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        public string PostHeadline { get; set; }
        public string PostContent { get; set; }
        public DateTime? TimeOfPosting { get; set; } = null;
    }
}
