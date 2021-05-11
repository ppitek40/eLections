using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eLections.Models;

namespace eLections.Controllers
{
    [Authorize(Roles="CanManageLands")]
    public class ConstituenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public ConstituenciesController()
        {
            _context = new ApplicationDbContext();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Constituencies
        public ActionResult Index()
        {
            return View("ConstituencyList");
        }
        
        // GET: Constituencies/Create
        public ActionResult Create()
        {
            return View("ConstituencyForm");
        }


        // POST: Constituencies/Create
        [HttpPost]
        public ActionResult Create(Constituency constituency)
        {
            if (!ModelState.IsValid)
            {
                return View("ConstituencyForm", constituency);
            }

            _context.Constituencies.Add(constituency);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Constituencies/Edit/{id}
        public ActionResult Edit(int id)
        {
            var constituency = _context.Constituencies.SingleOrDefault(l => l.Id == id);
            if (constituency == null)
            {
                return HttpNotFound();
            }

            return View("ConstituencyForm", constituency);
        }

        // POST: Constituencies/Create
        [HttpPost]
        public ActionResult Edit(Constituency constituency)
        {
            if (!ModelState.IsValid)
            {
                return View("ConstituencyForm", constituency);
            }

            var constituencyInDb = _context.Constituencies.SingleOrDefault(c => c.Id == constituency.Id);
            if (constituencyInDb == null)
            {
                return HttpNotFound();
            }

            _mapper.Map<Constituency, Constituency>(constituency, constituencyInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}