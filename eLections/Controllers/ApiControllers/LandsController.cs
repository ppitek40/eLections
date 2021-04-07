using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using eLections.Models;

namespace eLections.Controllers.ApiControllers
{
    public class LandsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public LandsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }
        // GET: /api/lands
        public IHttpActionResult GetLands()
        {
            return Ok(_context.Lands.ToList());
        }


        // DELETE: /api/lands/{id}
        [HttpDelete]
        public IHttpActionResult DeleteLands(int id)
        {
            var land = _context.Lands.SingleOrDefault(l => l.Id == id);
            if (land == null)
            {
                return NotFound();
            }

            _context.Lands.Remove(land);
            _context.SaveChanges();
            return Ok();
        }
    }
}
