using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
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
        [Display(Name = "Number of votes")]
        [Range(0,Int32.MaxValue)]
        public int? NumberOfVotes { get; set; }
        [Display(Name="Constituency")]
        public int ConstituencyId { get; set; }

        public String FullName
        {
            get
            {
                return Firstname + " " + Surname;
            }
        }
        [Display(Name="Party")]
        public int PartyId { get; set; }
        public Party Party { get; set; }
        public Constituency Constituency { get; set; }
    }
}