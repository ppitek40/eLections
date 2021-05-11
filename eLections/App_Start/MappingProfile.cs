using AutoMapper;
using eLections.Dtos;
using eLections.Models;

namespace eLections
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<CandidateDto, Candidate>();
            CreateMap<Candidate, Candidate>();
            CreateMap<Party, PartyDto>();
            CreateMap<PartyDto, Party>();
            CreateMap<Party, Party>();
            CreateMap<Constituency, Constituency>();
        }
    }
}