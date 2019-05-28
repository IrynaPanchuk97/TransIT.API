using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.ViewModels
{
    public class VehicleTypeMalfunctionGroup
    {
        public VehicleType VehicleType { get; set; }
        public (MalfunctionGroup group, ulong count) GroupCount { get; set; }
    }

    public class VehicleTypeMalfunctionGroupViewModel
    {
        public VehicleTypeDTO VehicleType { get; set; }
        public (MalfunctionGroupDTO group, ulong count) GroupCount { get; set; }
    }
}
