using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class DataController<TEntity, TEntityDTO> : FilterController<TEntity, TEntityDTO>
        where TEntity : class, IEntity, new()
        where TEntityDTO : class
    {
        private readonly ICrudService<TEntity> _dataService;
        
        public DataController(
            IMapper mapper,
            ICrudService<TEntity> dataService,
            IFilterService<TEntity> filterService) : base(filterService, mapper)
        {
            _dataService = dataService;
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
                var result = await _dataService.SearchAsync(search);
                if (result != null) 
                    return Json(_mapper.Map<IEnumerable<TEntityDTO>>(result));
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<TEntity>(obj);
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                entity.ModId = userId;
                entity.CreateId = userId;

                var createdEntity = await _dataService.CreateAsync(entity);
                if (createdEntity != null)
                    return CreatedAtAction(nameof(Create), _mapper.Map<TEntityDTO>(createdEntity));
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id, [FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var entity = _mapper.Map<TEntity>(obj);
                    var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    entity.Id = id;
                    entity.ModId = userId;

                    var result = await _dataService.UpdateAsync(entity);
                    if (result != null)
                        return NoContent();
                }
                catch (ConstraintException ex)
                {
                    return Conflict(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    return Conflict(ex.Message);
                }
            }         
            return BadRequest();
        }

        [DeleteExceptionFilter]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _dataService.DeleteAsync(id);
            return NoContent();
        }

        protected int GetUserId() => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
