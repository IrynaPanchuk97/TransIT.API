using Microsoft.AspNetCore.Mvc;

namespace TransIT.API.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}