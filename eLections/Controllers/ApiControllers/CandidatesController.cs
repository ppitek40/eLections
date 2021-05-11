using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

using AutoMapper;
using eLections.Dtos;
using eLections.Models;
using Microsoft.Ajax.Utilities;

namespace eLections.Controllers.ApiControllers
{
    public class CandidatesController : ApiController
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
        //GET /api/Candidates
        public IHttpActionResult GetCandidates(string query = null)
        {
            var candidatesQuery = _context.Candidates.Include(c => c.Constituency).Include(c => c.Party);
            if (!query.IsNullOrWhiteSpace())
            {
                candidatesQuery = candidatesQuery.Where(c => c.FullName.Contains(query));
            }

            var candidateDtos = candidatesQuery.ToList().Select(_mapper.Map<Candidate, CandidateDto>);
            return Ok(candidateDtos);
        }
        //GET /api/Candidates/{id}
        public IHttpActionResult GetCandidate(int id)
        {
            var candidate = _context.Candidates.SingleOrDefault(c => c.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            return Ok(candidate);
        }
        //POST /api/Candidates
        [HttpPost]
        public IHttpActionResult CreateCandidate(CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var candidate = _mapper.Map<CandidateDto, Candidate>(candidateDto);
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
            candidateDto.Id = candidate.Id;
            return Created(new Uri(Request.RequestUri + "/" + candidate.Id), candidateDto);
        }

        //PUT /api/Candidates/{id}
        [HttpPut]
        public IHttpActionResult UpdateCandidate(int id, CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var candidateInDb = _context.Candidates.SingleOrDefault(c => c.Id == id);

            if (candidateInDb == null)
            {
                return NotFound();
            }

            candidateDto.Id = candidateInDb.Id;
            _mapper.Map<CandidateDto, Candidate>(candidateDto, candidateInDb);
            _context.SaveChanges();
            return Ok(candidateDto);
        }

        public IHttpActionResult DeleteCandidate(int id)
        {
            var candidate = _context.Candidates.SingleOrDefault(c => c.Id == id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            _context.SaveChanges();
            return Ok();
        }
    }
}
