using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace eLections.Models
{
    public class Constituency
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }
        
        public int Seats { get; set; }
    }
}