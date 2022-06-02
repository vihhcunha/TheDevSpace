using Microsoft.AspNetCore.Mvc;

namespace TheDevSpaceWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}
