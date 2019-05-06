using System.Linq;
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
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : DataController<User, UserDTO>
    {
        private readonly IUserService _userService;
        
        public UserController(
            IMapper mapper, 
            IUserService userService,
            IODCrudService<User> odService
            ) : base(mapper, userService, odService)
        {
            _userService = userService;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] UserDTO obj)
        {
            return base.Update(id, obj);
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.UpdatePasswordAsync(id, changePassword);
                if (res != null) return NoContent();
            }
            return BadRequest();
        }
        
        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.ADMIN:
                    return await base.Get(offset, amount);
                case ROLE.ENGINEER:
                    var res = await _userService.GetAssignees(offset, amount);
                    if (res != null)
                        return Json(res.Select(x =>
                            _mapper.Map<UserDTO>(x)));
                    break;
            }

            return BadRequest();
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            return base.Create(obj);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
