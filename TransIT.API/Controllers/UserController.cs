using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    [Authorize(Roles = "ADMIN")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        
        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.GetRangeAsync(offset, amount);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.GetAsync(id);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            throw new NotImplementedException();
            if (ModelState.IsValid)
            {
                   
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.CreateAsync(
                    _mapper.Map<User>(obj));
                var res = _mapper.Map<UserDTO>(user);
                
                if (res != null)
                    return Created($"{Request.Path.Value}/{res.Id.ToString()}", res);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDTO obj)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.UpdateAsync(
                    _mapper.Map<User>(obj));
                var res = _mapper.Map<UserDTO>(user);

                if (res != null)
                    return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
