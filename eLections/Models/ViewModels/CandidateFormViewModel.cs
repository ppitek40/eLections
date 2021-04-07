using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eLections.Dtos;

namespace eLections.Models.ViewModels
{
    public class CandidateFormViewModel
    {
        public Candidate Candidate { get; set; }
        public IEnumerable<Land> Lands { get; set; }
        public IEnumerable<Party> Parties { get; set; }

    }
}