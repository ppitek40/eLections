using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eLections.Models
{
    public class PartyLandVotesMultiplier : PartyLandVotes
    {
        public int Multiplier
        {
            get => Multiplier;
            set => Multiplier = 1;
        }
    }
}