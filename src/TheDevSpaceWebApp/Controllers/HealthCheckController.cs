using Microsoft.AspNetCore.Mvc;

namespace TheDevSpaceWebApp.Controllers
{
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return Content("ok");
        }
    }
}
