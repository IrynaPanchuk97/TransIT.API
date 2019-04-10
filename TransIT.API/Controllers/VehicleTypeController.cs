using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;


namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class VehicleTypeController : DataController<VehicleType, VehicleTypeDTO>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IMapper mapper, IVehicleTypeService vehicleTypeService) : base(mapper, vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }
    }
}
