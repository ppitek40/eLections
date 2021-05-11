using System;
using System.Collections.Generic;
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
        private readonly ElectionHelper _electionHelper;
        public ElectionHelper(ApplicationDbContext context)
        {
            _context = context;
            _candidatesHelper = new CandidatesHelper(_context);
            _partyHelper = new PartyHelper(_context);
            _electionHelper = new ElectionHelper(_context);
        }

        public async Task<int> CalculateElections()
        {
            if (!_candidatesHelper.DoAllCandidatesHaveVotes())
            {
                return CalculationResult.NotAllowed;
            }
            var parties = _context.Parties.ToList();
            var qualifiedParties = new List<Party>();
            var lands = _context.Lands.ToList();

            var summaryVotes = await _candidatesHelper.SumVotes();


            foreach (var land in lands)
            {

                List<PartyLandVotesMultiplier> partyLandVotes = (await _partyHelper.SumPartyVotesInLandAsync(land.Id, summaryVotes))
                    .Where(p => p.LandId == land.Id)
                    .Cast<PartyLandVotesMultiplier>()
                    .ToList();

                for (int i = 0; i < land.Seats; i++)
                {
                    var maxValueOfVotes = partyLandVotes
                        .Max(p => p.Votes / p.Multiplier);
                    partyLandVotes.Find(p => p.Votes == maxValueOfVotes).Multiplier++;

                }

                var earnedSeats = partyLandVotes
                    .Where(p => p.Multiplier > 1)
                    .ToList();
                earnedSeats.ForEach(p => p.Multiplier -= 1);

                _candidatesHelper.GiveSeatsInLand(earnedSeats, land.Id);
                
            }
            return CalculationResult.OK;
        }

    }
}