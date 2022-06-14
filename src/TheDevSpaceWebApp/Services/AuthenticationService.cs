using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TheDevSpaceWebApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthenticationService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task LoginAsync(Guid userId, string email, string name, Guid? writerId = null)
        {
            var httpContext = _contextAccessor.HttpContext;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim("UserId", userId.ToString())
            };

            if (writerId.HasValue)
                claims.Add(new Claim("WriterId", writerId.Value.ToString()));

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.Now
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public async Task LogoutAsync()
        {
            var httpContext = _contextAccessor.HttpContext;

            await httpContext.SignOutAsync();
        }
    }
}
