using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransIT.BLL.Services;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class MalfunctionSubGroupController : DataController<MalfunctionSubgroup, MalfunctionSubgroupDTO>
    {
        private readonly IMalfunctionSubgroupService _malfunctionSubgroupService;

        public MalfunctionSubGroupController(
            IMapper mapper, 
            IMalfunctionSubgroupService malfunctionSubgroupService,
            IODCrudService<MalfunctionSubgroup> odService
            ) : base(mapper, malfunctionSubgroupService, odService)
        {
            _malfunctionSubgroupService = malfunctionSubgroupService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return base.Get(offset, amount);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }
    }
}
