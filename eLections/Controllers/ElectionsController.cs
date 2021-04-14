using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLections.Models;

namespace eLections.Controllers
{
    [Authorize(Roles="CanManageElections")]
    public class ElectionsController : Controller
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

        // GET: Elections
        public ActionResult Index()
        {
            return View(_context.Elections.ToList());
        }
    }
}