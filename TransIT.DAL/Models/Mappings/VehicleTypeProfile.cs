using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class VehicleTypeProfile : Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<VehicleTypeDTO, VehicleType>()
                .ForMember(v => v.ModId, opt => opt.Ignore())
                .ForMember(v => v.CreateId, opt => opt.Ignore())
                .ForMember(v => v.Mod, opt => opt.Ignore())
                .ForMember(v => v.Create, opt => opt.Ignore())
                .ForMember(v => v.Vehicle, opt => opt.Ignore())
                .ForMember(v => v.ModDate, opt => opt.Ignore())
                .ForMember(v => v.CreateDate, opt => opt.Ignore());
            CreateMap<VehicleType, VehicleTypeDTO>();
        }
    }
}
