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
        public String ShortName { get; set; }
        public String FullName { get; set; }
        public bool IsInSejm { get; set; }
    }
}