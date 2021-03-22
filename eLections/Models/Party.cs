using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class Party
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Party Name")]
        public String Name { get; set; }
        public Boolean IsInSejm { get; set; }
    }
}