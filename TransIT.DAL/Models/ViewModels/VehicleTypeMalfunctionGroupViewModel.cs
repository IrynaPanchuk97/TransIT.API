using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.ViewModels
{
    public class VehicleTypeMalfunctionGroupViewModel
    {
        public VehicleTypeDTO VehicleType { get; set; }
        public (MalfunctionGroupDTO group, ulong count) GroupCount { get; set; }
    }
}
