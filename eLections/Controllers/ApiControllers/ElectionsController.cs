using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using eLections.Helpers;
using eLections.Models;
using WebGrease.Css.Extensions;

namespace eLections.Controllers.ApiControllers
{
    [Authorize(Roles = "CanManageElections")]
    public class ElectionsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        private readonly CandidatesHelper _candidatesHelper;
        private readonly PartyHelper _partyHelper;
        private readonly ElectionHelper _electionHelper;

        public ElectionsController()
        {
            _context = new ApplicationDbContext();
            _candidatesHelper = new CandidatesHelper(_context);
            _partyHelper = new PartyHelper(_context);
            _electionHelper = new ElectionHelper(_context);
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        //GET /api/Elections
        public IHttpActionResult GetElections()
        {
            return Ok(_context.Elections.ToList());
        }
        //GET /api/Elections
        public IHttpActionResult GetElection(int id)
        {
            var election = _context.Elections.FirstOrDefault(e => e.Id == id);
            if (election == null)
            {
                return NotFound();
            }
            return Ok(election);
        }

        // POST: /api/Elections
        [HttpPost]
        public IHttpActionResult CreateElection()
        {
            if (_context.Elections.Any(e => e.EndOfElections == null))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }
            else
            {
                var election = new Election
                {
                    StartOfElections = DateTime.Now
                };
                _context.Elections.Add(election);
                _context.SaveChanges();
                return Created(new Uri(Request.RequestUri +"/"+election.Id), election);
            }
        }
        // POST: /api/Elections/Calculate/{id}
        [HttpPost]
        [Route("api/Elections/Calculate/{id}")]
        public async Task<IHttpActionResult> CalculateElectionsAsync(int id)
        {

            var election = _context.Elections.FirstOrDefault(e=>e.Id==id);
            if (election == null)
            {
                return NotFound();
            }

            await _electionHelper.CalculateElections();


            election.EndOfElections=DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }
    }
}
