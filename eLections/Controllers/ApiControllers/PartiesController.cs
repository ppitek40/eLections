using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eLections.Models;

namespace eLections.Controllers.ApiControllers
{
    public class PartiesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public PartiesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: /api/Parties
        public IHttpActionResult GetParties()
        {
            return Ok(_context.Parties.ToList());
        }

        // DELETE: /api/Parties/{id}
        public IHttpActionResult DeleteParty(int id)
        {
            var party = _context.Parties.SingleOrDefault(p => p.Id == id);
            if (party == null)
            {
                return NotFound();
            }

            _context.Parties.Remove(party);
            _context.SaveChanges();
            return Ok();
        }
    }
}
