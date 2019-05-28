using TransIT.DAL.Models.DTOs;

namespace TransIT.DAL.Models.ViewModels
{
    public class VehicleTypeMalfunctionSubGroupViewModel
    {
        public VehicleTypeDTO VehicleType { get; set; }
        public (MalfunctionSubgroupDTO subGroup, ulong count) SubGroupCount { get; set; }
    }
}
