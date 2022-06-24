using System.Net;

namespace TheDevSpaceWebApp.Middlewares
{
    public class CustomErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomErrorHandlingMiddleware(RequestDelegate next, ILogger<CustomErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                RedirectToErrorPage(httpContext);
            }
        }

        private void RedirectToErrorPage(HttpContext httpContext)
        {
            httpContext.Response.Redirect("/Error/" + httpContext.Response.StatusCode);
            httpContext.Response.CompleteAsync();
        }
    }

    public static class CustomErrorHandlingAppBuilderExtensions
    {
        public static void UseCustomErrorHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomErrorHandlingMiddleware>();
        }
    }
}
