using System;
using System.Collections.Generic;
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
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST")]
    public class IssueLogController : DataController<IssueLog, IssueLogDTO>
    {
        private readonly IIssueLogService _issueLogService;
        private const string IssueLogByIssueUrl = "~/api/v1/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog); 
        private const string DataTableTemplateIssueLogByIssueUrl = "~/api/v1/datatable/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog); 

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
                    return Json(
                        _mapper.Map<IEnumerable<IssueLogDTO>>(res)
                        );
            }
            return BadRequest();
        }
        
        [HttpPost(DataTableTemplateIssueLogByIssueUrl)]
        public virtual async Task<IActionResult> Filter(
            int issueId,
            DataTableRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = string.Empty;
                IEnumerable<IssueLogDTO> res;
                try
                {
                    res = await GetMappedEntitiesByIssueId(issueId, model);
                }
                catch (ArgumentException ex)
                {
                    res = null;
                    errorMessage = ex.Message;
                }

                var dtResponse = ComposeDataTableResponseViewModel(
                    res,
                    model,
                    errorMessage,
                    _filterService.TotalRecordsAmount()
                    );
                dtResponse.RecordsFiltered = (ulong) dtResponse.Data.LongLength;
                return Json(dtResponse);
            }
            return BadRequest();
        }

        private async Task<IEnumerable<IssueLogDTO>> GetMappedEntitiesByIssueId(int issueId, DataTableRequestViewModel model) =>
            _mapper.Map<IEnumerable<IssueLogDTO>>(
                await _filterService.GetQueriedWithWhereAsync(
                    model, 
                    x => x.IssueId == issueId
                    )
                );

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueLogDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<IssueLog>(obj);
                entity.CreateId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                entity = await _issueLogService.CreateAsync(entity);
                if (entity != null)
                    return CreatedAtAction(
                        nameof(Create),
                        _mapper.Map<IssueLogDTO>(entity));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public override async Task<IActionResult> Update(int id, [FromBody] IssueLogDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<IssueLog>(obj);
                entity.Id = id;
                entity.ModId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (await _issueLogService.UpdateAsync(entity) != null)
                    return NoContent();
            }
            return BadRequest();
        }
    }
}
