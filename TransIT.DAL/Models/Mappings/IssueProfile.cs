using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<IssueDTO, Issue>()
                .ForMember(i => i.AssignedToNavigation, opt => opt.MapFrom(i => i.AssignedTo))
                .ForMember(i => i.ModId, opt => opt.Ignore())
                .ForMember(i => i.CreateId, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Bill, opt => opt.Ignore())
                .ForMember(i => i.IssueLog, opt => opt.Ignore())
                .ForMember(i => i.StateId, opt => opt.Ignore())
                .ForMember(i => i.VehicleId, opt => opt.Ignore())
                .ForMember(i => i.MalfunctionId, opt => opt.Ignore())
                .ForMember(i => i.AssignedTo, opt => opt.Ignore());
            CreateMap<Issue, IssueDTO>();

        }
    }
}
