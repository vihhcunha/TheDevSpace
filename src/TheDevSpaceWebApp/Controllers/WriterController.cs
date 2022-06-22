using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.Services;
using TheDevSpaceWebApp.ViewModels.Writer;

namespace TheDevSpaceWebApp.Controllers;

public class WriterController : BaseController
{
    private readonly IWriterService _writerService;
    private readonly IAuthenticationService _authenticationService;
    public WriterController(IValidationService validationService, IWriterService writerService, IAuthenticationService authenticationService) : base(validationService)
    {
        _writerService = writerService;
        _authenticationService = authenticationService;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterWriterViewModel registerWriterViewModel)
    {
        if (!ModelState.IsValid) return View(registerWriterViewModel);

        var writer = await _writerService.AddWriter(new WriterDto
        {
            Age = registerWriterViewModel.Age,
            Description = registerWriterViewModel.Description,
            Role = registerWriterViewModel.Role,
            UserId = _authenticationService.UserId.GetValueOrDefault()
        });

        if (IsInvalidOperation()) return View(registerWriterViewModel);

        _authenticationService.AddClaim("WriterId", writer.WriterId.ToString());

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Writer/{writerId}")]
    public async Task<IActionResult> Details(Guid writerId)
    {
        var writer = await _writerService.GetWriterWithArticles(writerId);
        var writerViewModel = WriterViewModel.WriterDtoToViewModel(writer);
        return View(writerViewModel);
    }
}
