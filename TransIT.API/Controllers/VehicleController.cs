﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;


namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER")]
    public class VehicleController : DataController<Vehicle, VehicleDTO>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(
            IMapper mapper, 
            IVehicleService vehicleService,
            IODCrudService<Vehicle> odService
            ) : base(mapper, vehicleService, odService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] VehicleDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] VehicleDTO obj)
        {
            return base.Update(id, obj);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
