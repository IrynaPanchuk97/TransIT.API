using System;
using System.Linq;
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
    public class IssueLogController : DataController<IssueLog, IssueLogDTO>
    {
        private readonly IIssueLogService _issueLogService;
        
        public IssueLogController(IMapper mapper, IIssueLogService issueLogService) : base(mapper, issueLogService)
        {
            _issueLogService = issueLogService;
        }

        [HttpGet("/filter/{issueId}")]
        public virtual async Task<IActionResult> Get(int issueId)
        {
            if (ModelState.IsValid)
            {
                var res = await _issueLogService.GetRangeByIssueIdAsync(issueId);
                if (res != null)
                    return Json(res.Select(x =>
                        _mapper.Map<IssueLogDTO>(res)));
            }

            return BadRequest();
        }
    }
}
