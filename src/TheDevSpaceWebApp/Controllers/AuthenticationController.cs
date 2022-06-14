using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.ViewModels.Authentication;

namespace TheDevSpaceWebApp.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IValidationService _validationService;

        public AuthenticationController(IUserService userService, IValidationService validationService) : base(validationService)
        {
            _userService = userService;
            _validationService = validationService;
        }

        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet("Auth/Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            await _userService.AddUser(new UserDto
            {
                Email = registerViewModel.Email,
                Name = registerViewModel.Name,
                Password = registerViewModel.Password
            });

            if (IsInvalidOperation()) return View(registerViewModel);
            //TODO - Authenticate User

            return RedirectToAction("Register", "Writer");
        }
    }
}
