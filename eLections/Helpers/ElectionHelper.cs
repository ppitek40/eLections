using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using eLections.Models;

namespace eLections.Helpers
{
    public class ElectionHelper
    {
        private readonly ApplicationDbContext _context;
        private readonly CandidatesHelper _candidatesHelper;
        private readonly PartyHelper _partyHelper;
        
        public ElectionHelper(ApplicationDbContext context)
        {
            _context = context;
            _candidatesHelper = new CandidatesHelper(_context);
            _partyHelper = new PartyHelper(_context);
            
        }

        public async Task<int> CalculateElectionsAsync()
        {
            if (!_candidatesHelper.DoAllCandidatesHaveVotes())
            {
                return CalculationResult.NotAllowed;
            }
            var parties = _context.Parties.ToList();
            var qualifiedParties = new List<Party>();
            var constituencies = _context.Constituencies.ToList();

            var summaryVotes = await _candidatesHelper.SumVotes();


            foreach (var constituency in constituencies)
            {

                var partyConstituencyVotes2 = await _partyHelper.SumPartyVotesInConstituencyAsync(constituency.Id, summaryVotes);
                var partyConstituencyVotes= partyConstituencyVotes2.Where(p => p.ConstituencyId == constituency.Id).ToList();

                for (int i = 0; i < constituency.Seats; i++)
                {
                    var maxValueOfVotes = partyConstituencyVotes
                        .Max(p => p.Votes / p.Multiplier);
                    partyConstituencyVotes.Find(p => p.Votes/p.Multiplier == maxValueOfVotes).Multiplier++;
                }

                var earnedSeats = partyConstituencyVotes
                    .Where(p => p.Multiplier > 1)
                    .ToList();
                earnedSeats.ForEach(p => p.Multiplier -= 1);

                _candidatesHelper.GiveSeatsInConstituency(earnedSeats, constituency.Id);
                
            }
            return CalculationResult.OK;
        }

        public async Task PrepareNewElections()
        {
           var candidates= await _context.Candidates.ToListAsync();

           foreach (var candidate in candidates)
           {
               candidate.NumberOfVotes = null;
               candidate.IsInParliament = false;
           }
        }

    }
}