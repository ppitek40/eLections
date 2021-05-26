using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private readonly ElectionHelper _electionHelper;

        public ElectionsController()
        {
            _context = new ApplicationDbContext();
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
       
        // POST: /api/Elections
        [HttpPost]
        public async Task<IHttpActionResult> CreateElection()
        {
            if (await _context.Elections.AnyAsync(e => e.EndOfElections == null))
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }

            await _electionHelper.PrepareNewElections();
            
            var election = new Election
            {
                StartOfElections = DateTime.Now
            };
            _context.Elections.Add(election);
            await _context.SaveChangesAsync();
            return Created(new Uri(Request.RequestUri +"/"+election.Id), election);
        
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

            if (await _electionHelper.CalculateElectionsAsync() != CalculationResult.OK)
            {
                return BadRequest();
            }

            election.EndOfElections=DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
