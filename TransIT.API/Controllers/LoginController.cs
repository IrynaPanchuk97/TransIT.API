using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.ViewModels;
using TransIT.BLL.Services;

namespace TransIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthentificationService _authService;
        public LoginController(IAuthentificationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> RequestToken([FromBody]LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                string token = await _authService.Authentificate(loginModel.Login, loginModel.Password);
                if (token == null)
                {
                    return BadRequest(new { message = "Email or password is incorrect" });
                }

                return Ok(new { bearer = token });
            }

            return BadRequest(ModelState);
        }
    }
}