using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eLections.Models;

namespace eLections.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CandidatesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);

        }

        // GET: Candidates
        public ActionResult Index()
        {
            return View();
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            throw new NotImplementedException();
            return View();
        }
    }
}