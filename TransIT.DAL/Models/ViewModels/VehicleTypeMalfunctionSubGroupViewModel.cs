using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.ViewModels
{
    public class VehicleTypeMalfunctionSubgroup
    {
        public VehicleType VehicleType { get; set; }
        public MalfunctionSubgroup Subgroup { get; set; }
        public ulong Count { get; set; }
    }
    public class VehicleTypeMalfunctionSubGroupViewModel
    {
        public VehicleTypeDTO VehicleType { get; set; }
        public MalfunctionSubgroupDTO Subgroup { get; set; }
        public ulong Count { get; set; }
    }
}
