using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public Event()
        {
            Date = DateTime.Now;
        }
    }
}
