using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Helpers;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class FilterController<TEntity, TEntityDTO> : Controller
        where TEntity : class, IEntity, new()
        where TEntityDTO : class
    {
        protected const string DataTableTemplateUri = "~/api/v1/datatable/[controller]";
        protected readonly IFilterService<TEntity> _filterService;
        protected readonly IMapper _mapper;

        public FilterController(IFilterService<TEntity> filterService, IMapper mapper)
        {
            _filterService = filterService;
            _mapper = mapper;
        }

        [HttpPost(DataTableTemplateUri)]
        [Consumes("application/x-www-form-urlencoded")]
        public virtual async Task<IActionResult> Filter([FromForm] DataTableRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var errorMessage = string.Empty;
                TEntityDTO[] res = null;
                try
                {
                    res = _mapper.Map<IEnumerable<TEntityDTO>>(
                        await _filterService.GetQueriedAsync(model)
                        ).ToArray();
                }
                catch (ArgumentException ex)
                {
                    errorMessage = ex.Message;
                }

                var amount = _filterService.TotalRecordsAmount;
                return Json(new DataTableResponseViewModel
                {
                    Draw = (ulong) model.Draw,
                    Data = res,
                    RecordsTotal = amount,
                    RecordsFiltered = string.IsNullOrEmpty(model.Search.Value) ? amount : (ulong) res.Length,
                    Error = errorMessage
                });
            }

            return BadRequest();
        }
    }
}
