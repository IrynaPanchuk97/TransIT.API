using AutoMapper;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.DAL.Models.Mappings
{
    public class GroupStatisticProfile : Profile 
    {
        public GroupStatisticProfile()
        {
            CreateMap<VehicleTypeMalfunctionGroup, VehicleTypeMalfunctionGroupViewModel>();
            CreateMap<VehicleTypeMalfunctionGroupViewModel, VehicleTypeMalfunctionGroup>();
        }
    }
}
