using AutoMapper;
using Microsoft.Extensions.Options;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<IssueDTO, Issue>()
                .ForMember(i => i.AssignedToNavigation, opt => opt.Condition((dto, model) => dto.AssignedTo != null))
                .ForMember(i => i.AssignedTo, opt => opt.MapFrom(x => x.AssignedTo.Id))
                .ForMember(i => i.AssignedToNavigation, opt => opt.Ignore())
                .ForMember(i => i.ModId, opt => opt.Ignore())
                .ForMember(i => i.CreateId, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Bill, opt => opt.Ignore())
                .ForMember(i => i.IssueLog, opt => opt.Ignore())
                .ForMember(i => i.StateId, opt => opt.MapFrom(x => x.State.Id))
                .ForMember(i => i.State, opt => opt.Ignore())
                .ForMember(i => i.VehicleId, opt => opt.MapFrom(d => d.Vehicle == null ? 0 : d.Vehicle.Id))
                .ForMember(i => i.MalfunctionId, opt => opt.MapFrom(d => d.Malfunction == null ? 0 : d.Malfunction.Id))
                .ForMember(i => i.AssignedTo, opt => opt.MapFrom(i => i.AssignedTo.Id));
            CreateMap<Issue, IssueDTO>();

        }
    }
}
