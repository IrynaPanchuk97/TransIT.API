using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;


namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class VehicleTypeController : DataController<VehicleType, VehicleTypeDTO>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IMapper mapper, IVehicleTypeService vehicleTypeService) : base(mapper, vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return base.Get(offset, amount);
        }
    }
}

