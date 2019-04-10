using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        where TEntityDTO : class, new()
    {
        private readonly ICrudService<TEntity> _dataService;
        protected readonly IMapper _mapper;
        
        public DataController(IMapper mapper, ICrudService<TEntity> dataService)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.GetRangeAsync(offset, amount);
                if (res != null) 
                    return Json(res.Select(x =>
                        _mapper.Map<TEntityDTO>(x)));
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

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.SearchAsync(search);
                if (res != null) 
                    return Json(res.Select(x => 
                        _mapper.Map<TEntityDTO>(x)));
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
                    return CreatedAtRoute(
                        routeName: $"{Request.Path.Value}/{entity.Id.ToString()}",
                        routeValues: new {id = entity.Id},
                        value: _mapper.Map<TEntityDTO>(entity));
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
                entity = await _dataService.UpdateAsync(entity);
                if (entity != null)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            await _dataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
