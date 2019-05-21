﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.DTOs;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services.Interfaces;
using AutoMapper;
using TransIT.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class TransitionController : DataController<Transition, TransitionDTO>
    {
        private readonly ITransitionService _transitionService;
        public TransitionController(
            IMapper mapper,
            ITransitionService transitionService,
            IODCrudService<Transition> odService
            ) : base(mapper, transitionService, odService)
        {
            _transitionService = transitionService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] TransitionDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] TransitionDTO obj)
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
