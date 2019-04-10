using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
    public class MalfunctionGroupController : DataController<MalfunctionGroup, MalfunctionGroupDTO>
    {
        private IMalfunctionGroupService _malfunctionGroupService;

        public MalfunctionGroupController(IMapper mapper, IMalfunctionGroupService malfunctionGroupService) : base(mapper, malfunctionGroupService)
        {
            _malfunctionGroupService = malfunctionGroupService;
        }
    }
}
