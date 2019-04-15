using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    }
}
