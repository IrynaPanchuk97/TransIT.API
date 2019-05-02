using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class DataController<TEntity, TEntityDTO> : Controller
        where TEntity : class, IEntity, new()
        where TEntityDTO : class
    {
        protected const string ODataTemplateUri = "~/odata/[controller]";
        
        private readonly ICrudService<TEntity> _dataService;
        protected readonly IODCrudService<TEntity> _odService;
        protected readonly IMapper _mapper;
        
        public DataController(
            IMapper mapper,
            ICrudService<TEntity> dataService,
            IODCrudService<TEntity> odService)
        {
            _mapper = mapper;
            _dataService = dataService;
            _odService = odService;
        }

        [HttpGet(ODataTemplateUri)]
        [EnableQuery]
        public async Task<IActionResult> Get(ODataQueryOptions<TEntity> query)
        {
            if (ModelState.IsValid)
            {
                var res = await _odService.GetQueriedAsync(query);
                if (res != null)
                    return Json(_mapper.Map<IEnumerable<TEntityDTO>>(res));
            }
            return BadRequest();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.GetRangeAsync(offset, amount);
                if (res != null) 
                    return Json(_mapper.Map<IEnumerable<TEntityDTO>>(res));
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.GetAsync(id);
                if (res != null)
                    return Json(_mapper.Map<TEntityDTO>(res));
            }
            return BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.SearchAsync(search);
                if (res != null) 
                    return Json(_mapper.Map<IEnumerable<TEntityDTO>>(res));
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = await _dataService.CreateAsync(
                    _mapper.Map<TEntity>(obj));
                if (entity != null)
                    return CreatedAtAction(
                        nameof(Create),
                        _mapper.Map<TEntityDTO>(entity));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<TEntity>(obj);
                entity.Id = id;
                if (await _dataService.UpdateAsync(entity) != null)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dataService.DeleteAsync(id);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (ConstraintException ex)
            {
                return Conflict(ex.Message);
            }
            return NoContent();
        }
    }
}
