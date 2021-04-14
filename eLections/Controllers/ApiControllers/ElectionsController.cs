using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eLections.Models;

namespace eLections.Controllers.ApiControllers
{
    [Authorize(Roles = "CanManageElections")]
    public class ElectionsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ElectionsController()
        {
            _context = new ApplicationDbContext();
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
        public IHttpActionResult CalculateElections(int id)
        {
            var election = _context.Elections.FirstOrDefault(e=>e.Id==id);
            election.EndOfElections=DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }
    }
}
