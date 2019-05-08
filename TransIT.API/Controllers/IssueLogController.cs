using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,CUSTOMER,ANALYST")]
    public class IssueLogController : DataController<IssueLog, IssueLogDTO>
    {
        private readonly IIssueLogService _issueLogService;
        private const string IssueLogByIssueUrl = "~/api/v1/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog); 
        
        public IssueLogController(
            IMapper mapper,
            IIssueLogService issueLogService,
            IODCrudService<IssueLog> odService
            ) : base(mapper, issueLogService, odService)
        {
            _issueLogService = issueLogService;
        }

        [HttpGet(IssueLogByIssueUrl)]
        public virtual async Task<IActionResult> GetByIssue(int issueId)
        {
            if (ModelState.IsValid)
            {
                var res = await _issueLogService.GetRangeByIssueIdAsync(issueId);
                if (res != null)
                    return Json(res.Select(x =>
                        _mapper.Map<IssueLogDTO>(x)));
            }
            return BadRequest();
        }
    }
}
