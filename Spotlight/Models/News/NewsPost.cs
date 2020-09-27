using System;
using System.ComponentModel.DataAnnotations;

namespace Spotlight.Models
{
    public class NewsPost : Post
    {
        [Key]
        public int id { get; set; }
        
        public string UserID { get; set; }
    }
}