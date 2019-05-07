using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        protected const string ODataTemplateUri = "~/api/v1/odata/[controller]";
        protected const string DataTableTemplateUri = "~/api/v1/datatable/[controller]";
        protected readonly IODCrudService<TEntity> _filterService;
        protected readonly IMapper _mapper;

        public FilterController(IODCrudService<TEntity> filterService, IMapper mapper)
        {
            _filterService = filterService;
            _mapper = mapper;
        }

        
        [HttpGet(ODataTemplateUri)]
        public async Task<IActionResult> Get(ODataQueryOptions<TEntity> query)
        {
            if (ModelState.IsValid)
            {
                var res = await _filterService.GetQueriedAsync(query);
                if (res != null)
                    return Json(_mapper.Map<IEnumerable<TEntityDTO>>(res));
            }
            return BadRequest();
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
    }
}
