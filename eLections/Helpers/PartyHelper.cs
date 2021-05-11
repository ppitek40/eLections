using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using eLections.Models;

namespace eLections.Helpers
{
    public class PartyHelper
    {
        private readonly ApplicationDbContext _context;

        public PartyHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Party> GetParties()
        {
            return _context.Parties.ToList();
        }

        public bool IsPartyQualified(Party party, int summaryVotes)
        {
            var sum = _context.Candidates.Where(c => c.PartyId == party.Id).Sum(c=>c.NumberOfVotes.Value);
            

            if (party.IsCoalition)
            {
                return sum >= summaryVotes * 0.08;
            }
            else
            {
                return sum >= summaryVotes * 0.05;
            }
        }

        public async Task<List<PartyLandVotes>> SumPartyVotesInLandAsync(int landId,int summaryVotes)
        {
            var candidatesInLand = await _context.Candidates.Where(c => c.LandId == landId).ToListAsync();
            var PartyList = await _context.Parties.Where(p=>IsPartyQualified(p,summaryVotes)).ToListAsync();
            var PartyLandVotesList = new List<PartyLandVotes>();
            foreach (var party in PartyList)
            {
                var votes = candidatesInLand
                    .Where(c=>c.PartyId==party.Id)
                    .Sum(c=>c.NumberOfVotes.Value);
                

                PartyLandVotesList.Add(new PartyLandVotes
                {
                    PartyId = party.Id,
                    LandId = landId,
                    Votes = votes
                });
            }

            return PartyLandVotesList;
        }
    }
}