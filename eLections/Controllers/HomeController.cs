using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eLections.Models;
using eLections.Models.ViewModels;

namespace eLections.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            if (context.Elections.Any(e => e.EndOfElections == null))
            {
                return View();
            }

            var candidates = await context.Candidates.Where(c => c.IsInParliament).Include(c=>c.Party).Include(c=>c.Constituency).ToListAsync();
            var parties = candidates.Select(c => c.Party).Distinct().ToList();

            var viewModel = new ResultsViewModel
            {
                Candidates = candidates,
                Parties = parties
            };
            return View("IndexResults", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}