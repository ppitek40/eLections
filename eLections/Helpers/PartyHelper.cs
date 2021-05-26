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
                return  sum >= summaryVotes * 0.08;
            }
            else
            {
                return  sum >= summaryVotes * 0.05;
            }
        }

        public async Task<List<PartyConstituencyVotes>> SumPartyVotesInConstituencyAsync(int constituencyId, int summaryVotes)
        {
            var candidatesInConstituency =  await _context.Candidates.Where(c => c.ConstituencyId == constituencyId).ToListAsync();
            var partyList = _context.Parties.ToList();
            partyList= partyList.Where(p =>  IsPartyQualified(p, summaryVotes)).ToList();
            var partyConstituencyVotesList = new List<PartyConstituencyVotes>();
            foreach (var party in partyList)
            {
                var votes = ( candidatesInConstituency)
                    .Where(c=>c.PartyId==party.Id)
                    .Sum(c=>c.NumberOfVotes.Value);


                partyConstituencyVotesList.Add(new PartyConstituencyVotes
                {
                    PartyId = party.Id,
                    ConstituencyId = constituencyId,
                    Votes = votes,
                    Multiplier = 1
                });
            }

            return partyConstituencyVotesList;
        }
    }
}