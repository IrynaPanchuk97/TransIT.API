using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,WORKER,ENGINEER,CUSTOMER,ANALYST")]
    public class StateController : DataController<State, StateDTO>
    {
        private readonly IStateService _stateService;
        
        public StateController(
            IMapper mapper,
            IStateService stateService,
            IODCrudService<State> odService
            ) : base(mapper, stateService, odService)
        {
            _stateService = stateService;
        }

        [HttpPost]
        public override Task<IActionResult> Create([FromBody] StateDTO obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
        
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(int id, [FromBody] StateDTO obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }

        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
    }
}
