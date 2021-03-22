using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class Votes
    {
        public int Id { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Wartość musi być większa niż 0")]
        public int Amount { get; set; }
        public int? CandidateId { get; set; }
        public string CreatedBy { get; set; }

        public int ElectionsId { get; set; }

        public Candidate Candidate { get; set; }
        public Election Elections { get; set; }
    }
}