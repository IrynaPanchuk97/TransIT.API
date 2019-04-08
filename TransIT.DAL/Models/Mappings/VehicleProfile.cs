using AutoMapper;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.Mappings
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleDTO, Vehicle>()
                .ForMember(v => v.ModId, opt => opt.Ignore())
                .ForMember(v => v.CreateId, opt => opt.Ignore())
                .ForMember(v => v.Mod, opt => opt.Ignore())
                .ForMember(v => v.Create, opt => opt.Ignore())
                .ForMember(v => v.Issue, opt => opt.Ignore())
                .ForMember(v => v.InventoryId, opt => opt.Ignore())
                .ForMember(v => v.VehicleTypeId, opt => opt.Ignore())
                .ForMember(v => v.ModDate, opt => opt.Ignore())
                .ForMember(v => v.CreateDate, opt => opt.Ignore());
            CreateMap<Vehicle, VehicleDTO>();
        }
    }
}
