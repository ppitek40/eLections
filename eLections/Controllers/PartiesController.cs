using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eLections.Models;

namespace eLections.Controllers
{
    public class PartiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PartiesController()
        {
            _context = new ApplicationDbContext();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Parties
        public ActionResult Index()
        {
            return View("PartiesList");
        }


        // GET: Parties/Create
        public ActionResult Create()
        {
            return View("PartyForm");
        }

        // POST: Parties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Party party)
        {
            if (!ModelState.IsValid)
            {
                return View("PartyForm", party);
            }

            _context.Parties.Add(party);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Parties/Edit/{id}
        public ActionResult Edit(int id)
        {
            var party = _context.Parties.SingleOrDefault(p => p.Id == id);
            if (party == null)
            {
                return HttpNotFound();
            }

            return View("PartyForm", party);
        }

        // PUT: Parties/Edit/{id}
        [HttpPost]
        public ActionResult Edit(Party party)
        {
            if (!ModelState.IsValid)
            {
                return View("PartyForm", party);
            }

            var partyInDb = _context.Parties.SingleOrDefault(p => p.Id == party.Id);
            if (partyInDb == null)
            {
                return HttpNotFound();
            }

            _mapper.Map<Party, Party>(party, partyInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // DELETE: Parties/Delete/{id}
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var partyInDb = _context.Parties.SingleOrDefault(p => p.Id == id);
            if (partyInDb == null)
            {
                return HttpNotFound();
            }

            _context.Parties.Remove(partyInDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}