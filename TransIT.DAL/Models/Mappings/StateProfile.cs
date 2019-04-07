using AutoMapper;
using Remotion.Linq.Clauses.ResultOperators;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class StateProfile : Profile
    {
        public StateProfile()
        {
            CreateMap<StateDTO, State>()
                .ForMember(s => s.Issue, opt => opt.Ignore())
                .ForMember(s => s.IssueLogNewState, opt => opt.Ignore())
                .ForMember(s => s.IssueLogOldState, opt => opt.Ignore());
            CreateMap<State, StateDTO>();
        }
    }
}
