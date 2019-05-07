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
using TransIT.BLL.Services.InterfacesRepositories;
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
        [Consumes("application/x-www-form-urlencoded")]
        public override async Task<IActionResult> Filter([FromForm] DataTableRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = string.Empty;
                IssueDTO[] res = null;
                try
                {
                    var data = await _filterService.GetQueriedAsync(model);
                    
                    if (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value == ROLE.CUSTOMER)
                    {
                        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        data = data.Where(x => x.CreateId == userId);
                    }
                    res = _mapper.Map<IEnumerable<IssueDTO>>(data).ToArray();
                }
                catch (ArgumentException ex)
                {
                    errorMessage = ex.Message;
                }

                var totalAmount = _filterService.TotalRecordsAmount;
                return Json(new DataTableResponseViewModel
                {
                    Draw = (ulong) model.Draw,
                    Data = res,
                    RecordsTotal = totalAmount,
                    RecordsFiltered = string.IsNullOrEmpty(model.Search.Value)
                        ? totalAmount
                        : (ulong) res.Length,
                    Error = errorMessage
                });
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
                        res = await GetForEngineer(offset, amount);
                        break;
                }

                if (res != null)
                    return Json(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            if (ModelState.IsValid)
            {
                obj.State = null;

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
        
        [HttpPut("{id}")]
        public override async Task<IActionResult> Update(int id, [FromBody] IssueDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<Issue>(obj);
                entity.Id = id;
                entity.ModId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (await _issueService.UpdateAsync(entity) != null)
                    return NoContent();
            }
            return BadRequest();
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
            if (res != null)
                return res.Select(x => _mapper.Map<IssueDTO>(x));
            return null;
        }

        private async Task<IEnumerable<IssueDTO>> GetForEngineer(uint offset, uint amount)
        {
            var res = await _issueService.GetRangeAsync(offset, amount);
            if (res != null)
                return res.Select(x => _mapper.Map<IssueDTO>(x));
            return null;
        }
    }
}
