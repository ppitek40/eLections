using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class Candidate
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public String Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public String Surname { get; set; }
        public Boolean isInSejm { get; set; }
        public int RegionID { get; set; }

        public String FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }

        [Required]
        [Display(Name = "Partia")]
        public int PartyID { get; set; }
        public  Party Party { get; set; }
        public  Region Region { get; set; }
    }
}