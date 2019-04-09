using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public abstract class DataController<TEntity, TEntityDTO> : Controller
        where TEntity : class, IEntity, new()
        where TEntityDTO : class, new()
    {
        private readonly ICrudService<TEntity> _dataService;
        protected readonly IMapper Mapper;
        
        public DataController(IMapper mapper, ICrudService<TEntity> dataService)
        {
            Mapper = mapper;
            _dataService = dataService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.GetRangeAsync(offset, amount);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.GetAsync(id);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            if (ModelState.IsValid)
            {
                var res = await _dataService.SearchAsync(search);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = await _dataService.CreateAsync(
                    Mapper.Map<TEntity>(obj));
                var res = Mapper.Map<TEntityDTO>(entity);
                
                if (res != null)
                    return Created($"{Request.Path.Value}/{entity.Id.ToString()}", res);
            }
            return BadRequest();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] TEntityDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = await _dataService.UpdateAsync(
                    Mapper.Map<TEntity>(obj));
                var res = Mapper.Map<TEntityDTO>(entity);

                if (res != null)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _dataService.DeleteAsync(id);
            return NoContent();
        }
    }
}
