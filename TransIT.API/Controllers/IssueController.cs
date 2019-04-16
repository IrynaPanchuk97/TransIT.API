using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,CUSTOMER,ANALYST")]
    public class IssueController : DataController<Issue, IssueDTO>
    {
        private readonly IIssueService _issueService;
        private readonly IStateService _stateService;
        
        public IssueController(IMapper mapper, IIssueService issueService, IStateService stateService) : base(mapper, issueService)
        {
            _issueService = issueService;
            _stateService = stateService;
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Issue>(obj);
                entity.Vehicle = null;
                entity.Malfunction = null;
                entity.State = await _stateService.GetStateByNameAsync("new");

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                entity.CreateId = userId;

                entity = await _issueService.CreateAsync(entity);
                if (entity != null)
                    return CreatedAtAction(nameof(Create), _mapper.Map<IssueDTO>(entity));
            }
            return BadRequest();
        }
    }
}
