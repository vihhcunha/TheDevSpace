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

        public bool IsAuthenticated => _contextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public Guid? UserId 
        { 
            get
            {
                if (IsAuthenticated == false) return null;
                return Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue("UserId"));
            } 
        }

        public bool IsWriter
        {
            get
            {
                if (IsAuthenticated == false) return false;
                return _contextAccessor.HttpContext.User.HasClaim(c => c.Type == "WriterId");
            }
        }

        public Guid? WriterId
        {
            get
            {
                if (IsAuthenticated == false) return null;
                return Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(claimType: "WriterId"));
            }
        }

        public async Task LoginAsync(Guid userId, string email, string name, Guid? writerId = null)
        {
            var httpContext = _contextAccessor.HttpContext;
            ClaimsIdentity claimsIdentity = BuildClaims(userId, email, name, writerId);

            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTimeOffset.Now
            };

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static ClaimsIdentity BuildClaims(Guid userId, string email, string name, Guid? writerId)
        {
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
            return claimsIdentity;
        }

        public async Task LogoutAsync()
        {
            var httpContext = _contextAccessor.HttpContext;

            await httpContext.SignOutAsync();
        }

        public void AddClaim(string key, string value)
        {
            _contextAccessor.HttpContext.User.Claims.Append(new Claim(key, value));
        }
    }
}
