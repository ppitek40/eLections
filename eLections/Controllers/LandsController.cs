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
    public class LandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public LandsController()
        {
            _context = new ApplicationDbContext();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Lands
        public ActionResult Index()
        {
            return View("LandsList");
        }
        
        // GET: Lands/Create
        public ActionResult Create()
        {
            return View("LandForm");
        }


        // POST: Lands/Create
        [HttpPost]
        public ActionResult Create(Land land)
        {
            if (!ModelState.IsValid)
            {
                return View("LandForm", land);
            }

            _context.Lands.Add(land);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Lands/Edit/{id}
        public ActionResult Edit(int id)
        {
            var land = _context.Lands.SingleOrDefault(l => l.Id == id);
            if (land == null)
            {
                return HttpNotFound();
            }

            return View("LandForm",land);
        }

        // POST: Lands/Create
        [HttpPost]
        public ActionResult Edit(Land land)
        {
            if (!ModelState.IsValid)
            {
                return View("LandForm", land);
            }

            var landInDb = _context.Lands.SingleOrDefault(l => l.Id == land.Id);
            if (landInDb == null)
            {
                return HttpNotFound();
            }

            _mapper.Map<Land, Land>(land, landInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}