using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserController : DataController<User, UserDTO>
    {
        private readonly IUserService _userService;
        
        public UserController(IMapper mapper, IUserService userService) : base(mapper, userService)
        {
            _userService = userService;
        }
        
        [HttpPut("{id}")]
        public override async Task<IActionResult> Update(int id, [FromBody] UserDTO obj)
        {
            if (ModelState.IsValid)
            {
                var entity = _mapper.Map<User>(obj);
                entity.Id = id;
                if (await _userService.UpdateAsync(entity, false) != null)
                    return NoContent();
            }
            return BadRequest();
        }
    }
}
