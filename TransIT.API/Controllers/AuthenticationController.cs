using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.Abstractions;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]/[action]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService) =>
            _authenticationService = authenticationService;
        
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginViewModel credentials)
        {
            if (ModelState.IsValid)
            {
                var res = await _authenticationService.SignInAsync(credentials);
                if (res != null) return Json(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO token)
        {
            if (ModelState.IsValid)
            {
                var res = await _authenticationService.TokenAsync(token);
                return res == null
                    ? Forbid() as IActionResult
                    : Json(res);
            }
            return BadRequest();
        }
    }
}
