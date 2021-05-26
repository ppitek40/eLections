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
        [Display(Name = "Short Name")]
        public String ShortName { get; set; }
        [Required]
        [Display(Name="Full Name")]
        public String FullName { get; set; }
        [Display(Name = "Is Coalition")]
        public bool IsCoalition { get; set; }
    }
}