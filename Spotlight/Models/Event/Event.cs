using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Spotlight.Models.Identity;

namespace Spotlight.Models.Event
{
    public class Event
    {
        public int EventID { get; set; }

        [Required(ErrorMessage = "Please enter a product header")]
        public string Header { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        public string UserName { get; set; }
        public string UserID { get; set; }

        public virtual AppUser Sender { get; set; }

        public string ImageName { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }

        public Event()
        {
            Date = DateTime.Now;
        }
    }
}
