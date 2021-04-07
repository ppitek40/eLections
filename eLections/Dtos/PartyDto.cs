﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLections.Dtos
{
    public class PartyDto
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
    }
}