using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class MalfunctionProfile : Profile
    {
        public MalfunctionProfile()
        {
            CreateMap<MalfunctionDTO, Malfunction>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore())
                .ForMember(m => m.MalfunctionSubgroupId, opt => opt.Ignore());
            CreateMap<Malfunction, MalfunctionDTO>();
        }
    }
}