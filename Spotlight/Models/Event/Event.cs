using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models.Event
{
    public class Event
    {
        public int EventID { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
    }
}
