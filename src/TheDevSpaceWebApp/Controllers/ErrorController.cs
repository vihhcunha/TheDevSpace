using Microsoft.AspNetCore.Mvc;
using static TheDevSpaceWebApp.Models.ErrorViewModel;

namespace TheDevSpaceWebApp.Controllers;

public class ErrorController : Controller
{
    [HttpGet("Error/{statusCode}")]
    public IActionResult Index(int statusCode)
    {
        var errorViewModel = ErrorViewModelBuilder.BuildViewModel(statusCode);
        return View(errorViewModel);
    }
}
