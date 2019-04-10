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
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UserController : DataController<User, UserDTO>
    {
        private IUserService _userService;
        
        public UserController(IMapper mapper, IUserService userService) : base(mapper, userService)
        {
            _userService = userService;
        }   
    }
}
