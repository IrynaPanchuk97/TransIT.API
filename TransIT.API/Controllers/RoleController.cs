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
    public class RoleController : DataController<Role, string>
    {
        private IRoleService _roleService;
        
        public RoleController(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public virtual Task<IActionResult> Create([FromBody] Role obj)
        {
            return Task.FromResult(BadRequest() as IActionResult);
        }
        
        [HttpPut("{id}")]
        public virtual Task<IActionResult> Update(int id, [FromBody] Role obj)
        {
            return Task.FromResult(BadRequest() as IActionResult);
        }

        [HttpDelete("{id}")]
        public virtual Task<IActionResult> Delete(int id)
        {
            return Task.FromResult(BadRequest() as IActionResult);
        }
    }
}
