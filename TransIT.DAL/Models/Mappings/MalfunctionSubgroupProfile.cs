using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class MalfunctionSubgroupProfile : Profile
    {
        public MalfunctionSubgroupProfile()
        {
            CreateMap<MalfunctionSubgroupDTO, MalfunctionSubgroup>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.MalfunctionGroupId, opt => opt.MapFrom(x => x.MalfunctionGroup.Id))
                .ForMember(m => m.MalfunctionGroup, opt => opt.Ignore());
            CreateMap<MalfunctionSubgroup, MalfunctionSubgroupDTO>();
        }        
    }
}
