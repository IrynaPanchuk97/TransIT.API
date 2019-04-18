using System;
using System.Linq;
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
        
        public IssueController(IMapper mapper, IIssueService issueService) : base(mapper, issueService)
        {
            _issueService = issueService;
        }

        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
                if (res != null)
                    return Json(res.Select(x =>
                        _mapper.Map<IssueDTO>(x)));
            }
            return BadRequest();
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Issue>(obj);
                entity.Vehicle = null;
                entity.Malfunction = null;

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
