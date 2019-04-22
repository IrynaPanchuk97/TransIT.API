﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;


namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,CUSTOMER")]
    public class VehicleController : DataController<Vehicle, VehicleDTO>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IMapper mapper, IVehicleService vehicleService) : base(mapper, vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return base.Get(offset, amount);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override async Task<IActionResult> Create([FromBody] VehicleDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Vehicle>(obj);
                entity.VehicleType = null;

                entity = await _vehicleService.CreateAsync(entity);
                if (entity != null)
                    return CreatedAtAction(nameof(Create), _mapper.Map<IssueDTO>(entity));
            }
            return BadRequest();
        }
    }
}
