using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class IssueLogProfile : Profile
    {
        public IssueLogProfile()
        {
            CreateMap<IssueLogDTO, IssueLog>()
                .ForMember(i => i.ModId, opt => opt.Ignore())
                .ForMember(i => i.CreateId, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Document, opt => opt.Ignore())
                .ForMember(i => i.SupplierId, opt => opt.Ignore())
                .ForMember(i => i.NewStateId, opt => opt.Ignore())
                .ForMember(i => i.OldStateId, opt => opt.Ignore())
                .ForMember(i => i.ActionTypeId, opt => opt.Ignore());
            CreateMap<IssueLog, IssueLogDTO>();
        }
    }
}
