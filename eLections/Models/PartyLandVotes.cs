using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class PartyLandVotes
    {
        [Key, Column(Order=0)]
        public int PartyId { get; set; }
        public Party Party { get; set; }
        [Key, Column(Order=1)]
        public int LandId { get; set; }
        public Land Land { get; set; }
        public int Votes { get; set; }
    }
}