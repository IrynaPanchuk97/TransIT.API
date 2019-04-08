using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierDTO, Supplier>()
                .ForMember(s => s.ModDate, opt => opt.Ignore())
                .ForMember(s => s.CreateDate, opt => opt.Ignore())
                .ForMember(s => s.ModId, opt => opt.Ignore())
                .ForMember(s => s.CreateId, opt => opt.Ignore())
                .ForMember(s => s.Mod, opt => opt.Ignore())
                .ForMember(s => s.Create, opt => opt.Ignore())
                .ForMember(s => s.IssueLog, opt => opt.Ignore());
            CreateMap<Supplier, SupplierDTO>();
        }
    }
}
