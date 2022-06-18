using Microsoft.AspNetCore.Mvc;
using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpaceWebApp.Services;
using TheDevSpaceWebApp.ViewModels.Authentication;

namespace TheDevSpaceWebApp.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _loginService;

        public AuthenticationController(IUserService userService, IValidationService validationService, IAuthenticationService loginService) : base(validationService)
        {
            _userService = userService;
            _loginService = loginService;
        }

        [HttpGet("Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Auth/Login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var userDto = await _userService.LoginUser(new UserDto
            {
                Email = loginViewModel.Email,
                Password = loginViewModel.Password
            });

            if (IsInvalidOperation() || userDto == null) return View(loginViewModel);

            await _loginService.LoginAsync(userDto.UserId, userDto.Email, userDto.Name, userDto.WriterId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Auth/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _loginService.LogoutAsync();
            return RedirectToAction("Index", "Home");
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

            var userDto = await _userService.AddUser(new UserDto
            {
                Email = registerViewModel.Email,
                Name = registerViewModel.Name,
                Password = registerViewModel.Password
            });

            if (IsInvalidOperation()) return View(registerViewModel);

            await _loginService.LoginAsync(userDto.UserId, userDto.Email, userDto.Name, userDto.WriterId);

            if (registerViewModel.RedirectToWriterRegistration)
                return RedirectToAction("Register", "Writer");

            return RedirectToAction("Index", "Home");
        }
    }
}
