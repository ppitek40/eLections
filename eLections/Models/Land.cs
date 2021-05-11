using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace eLections.Models
{
    public class Land
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Region")]
        public String Name { get; set; }
        
        public int Seats { get; set; }
    }
}