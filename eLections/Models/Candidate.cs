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
        public int Id { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public String Firstname { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public String Surname { get; set; }
        public Boolean IsInSejm { get; set; }
        public int LandId { get; set; }

        public String FullName
        {
            get
            {
                return Firstname + " " + Surname;
            }
        }

        public int PartyId { get; set; }
        public Party Party { get; set; }
        public Land Land { get; set; }
    }
}