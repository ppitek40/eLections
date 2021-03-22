﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class Election
    {
        public int Id { get; set; }
        public Boolean IsClosed { get; set; }
        public DateTime EndOfElections { get; set; }
    }
}