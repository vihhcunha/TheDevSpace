using Microsoft.EntityFrameworkCore;
using TheDevSpace.Application.Mappings;
using TheDevSpace.Repository;
using TheDevSpaceWebApp.DI;
using TheDevSpace.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using TheDevSpaceWebApp.Middlewares;

namespace TheDevSpaceWebApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("TheDevSpaceConnectionString");

        services.AddHttpContextAccessor();
        services.UseCustomHashPasswordBuilder()
            .UseArgon2<UserDto>();

        services.AddDbContext<TheDevSpaceContext>(c =>
        {
            c.UseSqlServer(connectionString,
                sqlServer =>
                {
                    sqlServer.EnableRetryOnFailure();
                });
            c.LogTo(Console.WriteLine);
            c.EnableDetailedErrors();
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.AccessDeniedPath = "/Auth/Login";
                opt.LoginPath = "/Auth/Login";
            });

        services.AddAutoMapper(typeof(MapperProfile));
        services.ResolveServices();
        services.AddControllersWithViews();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseHsts();
        }
        app.UseCustomErrorHandling();
        app.UseStatusCodePagesWithRedirects("/Error/{0}");

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(e =>
        {
            e.MapControllerRoute(name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
