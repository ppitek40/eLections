using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using eLections.Models;

namespace eLections.Dtos
{
    public class CandidateDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Firstname")]
        public String Firstname { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public String Surname { get; set; }

        public String FullName
        {
            get { return Firstname + " " + Surname; }
        }
        public int LandId { get; set; }

        public int PartyId { get; set; }
        public Party Party { get; set; }
        public Land Land { get; set; }
    }
}