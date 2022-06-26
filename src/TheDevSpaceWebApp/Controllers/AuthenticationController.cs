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
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IUserService userService, IValidationService validationService, IAuthenticationService loginService) : base(validationService)
        {
            _userService = userService;
            _authenticationService = loginService;
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

            await _authenticationService.LoginAsync(userDto.UserId, userDto.Email, userDto.Name, userDto.Writer?.WriterId);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Auth/Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.LogoutAsync();
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

            await _authenticationService.LoginAsync(userDto.UserId, userDto.Email, userDto.Name, userDto.Writer?.WriterId);

            if (registerViewModel.RedirectToWriterRegistration)
                return RedirectToAction("Register", "Writer");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> UserSettings()
        {
            var user = await _userService.GetUser(_authenticationService.UserId.GetValueOrDefault());
            return View(new UserSettingsViewModel
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                IsWriter = user.Writer != null,
                Age = user.Writer?.Age,
                Description = user.Writer?.Description,
                WriterId = user.Writer?.WriterId,
                Role = user.Writer?.Role
            });
        }

        [HttpPost]
        public async Task<IActionResult> UserSettings(UserSettingsViewModel userSettingsViewModel)
        {
            if (!ModelState.IsValid) return View(userSettingsViewModel);

            await _userService.UpdateUser(new UserDto
            {
                UserId = userSettingsViewModel.UserId,
                Email = userSettingsViewModel.Email,
                Name = userSettingsViewModel.Name,
                Password = userSettingsViewModel.Password,
                Writer = new WriterDto
                {
                    Age = userSettingsViewModel.Age.GetValueOrDefault(),
                    Description = userSettingsViewModel.Description,
                    Role = userSettingsViewModel.Role,
                    WriterId = userSettingsViewModel.WriterId.GetValueOrDefault()
                }
            });

            AddValidationData();

            return View(userSettingsViewModel);
        }
    }
}
