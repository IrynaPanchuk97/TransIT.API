using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            IODCrudService<Issue> odService
            ) : base(mapper, issueService, odService)
        {
            _issueService = issueService;
        }

        [HttpPost(DataTableTemplateUri)]
        public override async Task<IActionResult> Filter(DataTableRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isCustomer = User.FindFirst(ROLE.ROLE_SCHEMA)?.Value == ROLE.CUSTOMER;
                var errorMessage = string.Empty;
                IssueDTO[] res = null;
                try
                {
                    res = _mapper.Map<IEnumerable<IssueDTO>>(
                        isCustomer
                            ? await _filterService.GetQueriedWithWhereAsync(model, x => x.CreateId == GetUserId())
                            : await _filterService.GetQueriedAsync(model)
                        ).ToArray();
                }
                catch (ArgumentException ex)
                {
                    errorMessage = ex.Message;
                }

                return Json(
                    ComposeDataTableResponseViewModel(
                            res,
                            model,
                            errorMessage,
                            isCustomer
                                ? _filterService.TotalRecordsAmount(x => x.CreateId == GetUserId())
                                : _filterService.TotalRecordsAmount()
                            )
                    );
            }

            return BadRequest();
        }
        
        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IssueDTO> res = null;

                switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
                {
                    case ROLE.CUSTOMER:
                        res = await GetForCustomer(offset, amount);
                        break;
                    case ROLE.ENGINEER:                        
                    case ROLE.ANALYST:
                        res = await GetIssues(offset, amount);
                        break;
                }
                if (res != null)
                    return Json(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public override Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            obj.State = null;
            return base.Create(obj);
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
            if (res != null)
                return _mapper.Map<IEnumerable<IssueDTO>>(res);
            return null;
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            var res = await _issueService.GetRangeAsync(offset, amount);
            if (res != null)
                return _mapper.Map<IEnumerable<IssueDTO>>(res);
            return null;
        }        
    }
}
