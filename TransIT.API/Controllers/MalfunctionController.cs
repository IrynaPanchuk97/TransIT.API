using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class MalfunctionController : DataController<Malfunction, MalfunctionDTO>
    {
        private readonly IMalfunctionService _malfunctionService;

        public MalfunctionController(IMapper mapper, IMalfunctionService malfunctionService) : base(mapper, malfunctionService)
        {
            _malfunctionService = malfunctionService;
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
