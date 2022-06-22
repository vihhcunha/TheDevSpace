using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.Services;
using TheDevSpaceWebApp.ViewModels.Writer;

namespace TheDevSpaceWebApp.Controllers;

public class WriterController : BaseController
{
    private readonly IWriterService _writerService;
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authenticationService;
    public WriterController(IValidationService validationService, IWriterService writerService, IAuthenticationService authenticationService, IUserService userService) : base(validationService)
    {
        _writerService = writerService;
        _authenticationService = authenticationService;
        _userService = userService;
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

        var userDto = await _userService.GetUser(writer.UserId);
        await _authenticationService.LoginAsync(userDto.UserId, userDto.Email, userDto.Name, writer.WriterId);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Writer/Details/{writerId}")]
    public async Task<IActionResult> Details(Guid writerId)
    {
        var writer = await _writerService.GetWriterWithArticles(writerId);

        if (writer == null)
        {
            ModelState.AddModelError(String.Empty, "This writer does not exists!");
            return View(writer);
        }
        var writerViewModel = WriterViewModel.WriterDtoToViewModel(writer);
        return View(writerViewModel);
    }
}
