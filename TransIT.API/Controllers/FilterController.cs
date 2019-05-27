using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
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
        
        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateUri)]
        public virtual async Task<IActionResult> Filter(DataTableRequestViewModel model) =>
            Json(
                ComposeDataTableResponseViewModel(
                    await GetMappedEntitiesByModel(model),
                    model,
                    _filterService.TotalRecordsAmount()
                    )
                );

        protected async Task<IEnumerable<TEntityDTO>> GetMappedEntitiesByModel(DataTableRequestViewModel model) =>
            _mapper.Map<IEnumerable<TEntityDTO>>(
                await _filterService.GetQueriedAsync(model)
                );

        protected virtual DataTableResponseViewModel ComposeDataTableResponseViewModel(
            IEnumerable<TEntityDTO> res,
            DataTableRequestViewModel model,
            ulong totalAmount,   
            string errorMessage = "") =>
            new DataTableResponseViewModel
            {
                Draw = (ulong) model.Draw,
                Data = res.ToArray(),
                RecordsTotal = totalAmount,
                RecordsFiltered =
                    model.Filters != null
                    && model.Filters.Any()
                    || model.Search != null
                    && !string.IsNullOrEmpty(model.Search.Value)
                        ? (ulong) res.Count()
                        : totalAmount,
                Error = errorMessage
            };
    }
}
