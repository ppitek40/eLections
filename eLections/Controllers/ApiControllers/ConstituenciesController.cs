using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eLections.Models;

namespace eLections.Controllers.ApiControllers
{
    public class ConstituenciesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ConstituenciesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: /api/constituencies
        public IHttpActionResult GetConstituencies()
        {
            return Ok(_context.Constituencies.ToList());
        }


        // DELETE: /api/constituencies/{id}
        [HttpDelete]
        public IHttpActionResult DeleteConstituencies(int id)
        {
            var constituency = _context.Constituencies.SingleOrDefault(c => c.Id == id);
            if (constituency == null)
            {
                return NotFound();
            }

            _context.Constituencies.Remove(constituency);
            _context.SaveChanges();
            return Ok();
        }
    }
}
