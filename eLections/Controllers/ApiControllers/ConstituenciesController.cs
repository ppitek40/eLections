using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
        public async Task<IHttpActionResult> GetConstituencies()
        {
            return Ok(await _context.Constituencies.ToListAsync());
        }


        // DELETE: /api/constituencies/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteConstituencies(int id)
        {
            var constituency = await _context.Constituencies.SingleOrDefaultAsync(c => c.Id == id);
            if (constituency == null)
            {
                return NotFound();
            }

            _context.Constituencies.Remove(constituency);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
