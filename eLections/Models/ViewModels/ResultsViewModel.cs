using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eLections.Controllers;

namespace eLections.Models.ViewModels
{
    public class ResultsViewModel
    {
        public IEnumerable<Candidate> Candidates { get; set; }
        public IEnumerable<Party> Parties { get; set; }


    }
}