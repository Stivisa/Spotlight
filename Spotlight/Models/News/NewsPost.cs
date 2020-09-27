using System;
using System.ComponentModel.DataAnnotations;

namespace Spotlight.Models
{
    public class NewsPost : Post
    {        
        public string UserID { get; set; }
    }
}