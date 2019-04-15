using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RoleController : DataController<Role, string>
    {
        private readonly IRoleService _roleService;
        
        public RoleController(IMapper mapper, IRoleService roleService) : base(mapper, roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public override Task<IActionResult> Create([FromBody] string obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
        
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(int id, [FromBody] string obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }

        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(int id)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
    }
}
