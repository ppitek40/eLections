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

        public List<Candidate> GetCandidatesFromLand(int landId)
        {
            return _context.Candidates.Where(c => c.LandId == landId).ToList();
        }

        public int GetSumOfVotesOfPartyInLand(int partyId, int landId)
        {
            return _context.Candidates
                .Where(c => c.PartyId == partyId && c.LandId == landId)
                .Sum(c => c.NumberOfVotes.Value);
        }

        public async Task<int> SumVotes()
        {
            var summaryVotes =  _context.Candidates.Sum(c => c.NumberOfVotes.Value);
            return summaryVotes;
        }

        public void GiveSeatsInLand(List<PartyLandVotesMultiplier> partyLandVotes, int landId)
        {
            foreach (var party in partyLandVotes)
            {
                var candidates = _context.Candidates
                    .Where(c => c.PartyId == party.PartyId && c.LandId==party.LandId)
                    .OrderByDescending(c=>c.NumberOfVotes.Value)
                    .Take(party.Multiplier);
                candidates.ForEach(c => c.IsInSejm = true);

            }

            _context.SaveChanges();
        }
    }
}