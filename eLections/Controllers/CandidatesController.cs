using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eLections.Dtos;
using eLections.Models;
using eLections.Models.ViewModels;

namespace eLections.Controllers
{
    [Authorize(Roles="CanManageCandidates")]
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CandidatesController()
        {
            _context = new ApplicationDbContext();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);

        }

        // GET: Candidates
        public ActionResult Index()
        {
            return View("CandidatesList");
        }

        // GET: Candidates/Create
        public async Task<ActionResult> Create()
        {
            var viewModel = new CandidateFormViewModel
            {
                Candidate = null,
                Constituencies = await _context.Constituencies.OrderBy(c=>c.Name).ToListAsync(),
                Parties = await _context.Parties.ToListAsync()

            };
            return View("CandidateForm", viewModel);
        }


        //POST: Candidates/Create
        [HttpPost]
        public async Task<ActionResult> Create(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CandidateFormViewModel
                {
                    Candidate = candidate,
                    Constituencies = await _context.Constituencies.OrderBy(c => c.Name).ToListAsync(),
                    Parties = await _context.Parties.ToListAsync()
                };
                return View("CandidateForm", viewModel);
            }

            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        //GET: Candidates/Edit/{id}
        public async Task<ActionResult> Edit(int id)
        {
            var candidate = await _context.Candidates.SingleOrDefaultAsync(c => c.Id == id);
            if (candidate == null)
            {
                return HttpNotFound();
            }


            var viewModel = new CandidateFormViewModel
            {
                Candidate = candidate,
                Constituencies = await _context.Constituencies.OrderBy(c => c.Name).ToListAsync(),
                Parties = await _context.Parties.ToListAsync()
            };
            return View("CandidateForm", viewModel);
        }

        //PUT: Candidates/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Edit(Candidate candidate)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CandidateFormViewModel
                {
                    Candidate = candidate,
                    Constituencies = await _context.Constituencies.OrderBy(c => c.Name).ToListAsync(),
                    Parties = await _context.Parties.ToListAsync()
                };
                return View("CandidateForm", viewModel);
            }

            var candidateInDb = await _context.Candidates.SingleOrDefaultAsync(c => c.Id == candidate.Id);
            if (candidateInDb == null)
            {
                return HttpNotFound();
            }

            _mapper.Map<Candidate, Candidate>(candidate, candidateInDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // DELETE: /Candidates/Delete/{id}
        [HttpDelete]
        public async  Task<ActionResult> Delete(int id)
        {
            var candidateInDb = await _context.Candidates.SingleOrDefaultAsync(c => c.Id == id);
            if (candidateInDb == null)
            {
                return HttpNotFound();
            }

            _context.Candidates.Remove(candidateInDb);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: /Candidates/Votes/{id}
        public async Task<ActionResult> Votes(int id)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
            if (candidate == null)
            {
                return HttpNotFound();
            }

            return View(candidate);
        }
        // POST: /Candidates/Votes/{id}
        [HttpPost]
        public async Task<ActionResult> Votes(Candidate candidate, int id)
        {
            var candidateInDb = await _context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
            if (candidateInDb == null)
            {
                return HttpNotFound();
            }

            candidateInDb.NumberOfVotes = candidate.NumberOfVotes;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}