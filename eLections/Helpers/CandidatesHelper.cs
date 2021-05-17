using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using eLections.Models;
using Microsoft.Ajax.Utilities;

namespace eLections.Helpers
{
    public class CandidatesHelper
    {
        private readonly ApplicationDbContext _context;

        public CandidatesHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Candidate> GetMembers()
        {
            return _context.Candidates.ToList();
        }

        public bool DoAllCandidatesHaveVotes()
        {
            return !_context.Candidates.Any(c => c.NumberOfVotes == null);
        }

        public List<Candidate> GetCandidatesFromConstituency(int constituencyId)
        {
            return _context.Candidates.Where(c => c.ConstituencyId == constituencyId).ToList();
        }

        public int GetSumOfVotesOfPartyInConstituency(int partyId, int constituencyId)
        {
            return _context.Candidates
                .Where(c => c.PartyId == partyId && c.ConstituencyId == constituencyId)
                .Sum(c => c.NumberOfVotes.Value);
        }

        public async Task<int> SumVotes()
        {
            var summaryVotes = await _context.Candidates.SumAsync(c => c.NumberOfVotes.Value);
            return summaryVotes;
        }

        public void GiveSeatsInConstituency(List<PartyConstituencyVotesMultiplier> partyConstituencyVotes, int constituencyId)
        {
            foreach (var party in partyConstituencyVotes)
            {
                var candidates = _context.Candidates
                    .Where(c => c.PartyId == party.PartyId && c.ConstituencyId==party.ConstituencyId)
                    .OrderByDescending(c=>c.NumberOfVotes.Value)
                    .Take(party.Multiplier);
                candidates.ForEach(c => c.IsInSejm = true);

            }

            _context.SaveChanges();
        }
    }
}