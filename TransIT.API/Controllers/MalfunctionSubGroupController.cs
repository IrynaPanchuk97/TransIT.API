﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class MalfunctionSubGroupController : DataController<MalfunctionSubgroup, MalfunctionSubgroupDTO>
    {
        private readonly MalfunctionSubgroupService _malfunctionSubgroupService;

        public MalfunctionSubGroupController(IMapper mapper, MalfunctionSubgroupService malfunctionSubgroupService) : base(mapper, malfunctionSubgroupService)
        {
            _malfunctionSubgroupService = malfunctionSubgroupService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            return base.Get(offset, amount);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }
    }
}
