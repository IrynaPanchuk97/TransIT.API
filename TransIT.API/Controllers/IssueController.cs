using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.API.Extensions;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,CUSTOMER,ANALYST")]
    public class IssueController : DataController<Issue, IssueDTO>
    {
        private readonly IIssueService _issueService;
        
        public IssueController(
            IMapper mapper, 
            IIssueService issueService,
            IFilterService<Issue> odService
            ) : base(mapper, issueService, odService)
        {
            _issueService = issueService;
        }

        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateUri)]
        public override async Task<IActionResult> Filter(DataTableRequestViewModel model)
        {
            var isCustomer = User.FindFirst(ROLE.ROLE_SCHEMA)?.Value == ROLE.CUSTOMER;
            var userId = GetUserId();

            return Json(
                ComposeDataTableResponseViewModel(
                    await GetQueryiedForSpecificUser(model, userId, isCustomer),
                    model,
                    GetTotalRecordsForSpecificUser(userId, isCustomer)
                    )
                );
        }
        
        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestViewModel model,
            int userId,
            bool isCustomer) =>
            _mapper.Map<IEnumerable<IssueDTO>>(
                isCustomer
                    ? await _filterService.GetQueriedWithWhereAsync(model, x => x.CreateId == userId)
                    : await _filterService.GetQueriedAsync(model)
                );

        private ulong GetTotalRecordsForSpecificUser(
            int userId,
            bool isCustomer) =>
            isCustomer
                ? _filterService.TotalRecordsAmount(x => x.CreateId == userId)
                : _filterService.TotalRecordsAmount();

        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.CUSTOMER:
                    return Json(await GetForCustomer(offset, amount));
                case ROLE.ENGINEER:                        
                case ROLE.ANALYST:
                    return Json(await GetIssues(offset, amount));
                default:
                    return BadRequest();
            }
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
            return res != null
                ? _mapper.Map<IEnumerable<IssueDTO>>(res)
                : null;
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            var res = await _issueService.GetRangeAsync(offset, amount);
            return res != null
                ? _mapper.Map<IEnumerable<IssueDTO>>(res)
                : null;
        }
    }
}
