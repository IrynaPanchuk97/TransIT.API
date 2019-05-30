using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.DAL.Models.ViewModels
{
    public class VehicleTypeMalfunctionGroup
    {
        public VehicleType VehicleType { get; set; }
        public MalfunctionGroup Group { get; set; }
        public ulong Count { get; set; }
    }

    public class VehicleTypeMalfunctionGroupViewModel
    {
        public VehicleTypeDTO VehicleType { get; set; }
        public MalfunctionGroupDTO Group {get; set;}
        public ulong Count { get; set; }
    }
}
