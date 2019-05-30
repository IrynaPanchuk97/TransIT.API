using AutoMapper;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.DAL.Models.Mappings
{
    public class SubgroupStatisticProfile: Profile
    {
        public SubgroupStatisticProfile()
        {
            CreateMap<VehicleTypeMalfunctionSubgroup, VehicleTypeMalfunctionSubGroupViewModel>();
            CreateMap<VehicleTypeMalfunctionSubGroupViewModel, VehicleTypeMalfunctionSubgroup>();
        }
    }
}
