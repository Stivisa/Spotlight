﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spotlight.Models
{
    public class Listing : Post
    {
        [Key]
        public int id { get; set; }
    }
}