using Microsoft.EntityFrameworkCore;
using TheDevSpace.Application.Mappings;
using TheDevSpace.Repository;
using TheDevSpaceWebApp.DI;
using TheDevSpace.Application;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        services.AddAutoMapper(typeof(MapperProfile));
        services.ResolveServices();
        services.AddControllersWithViews();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.AccessDeniedPath = "/Auth/Login";
                opt.LoginPath = "/Auth/Login";
            });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();
        app.UseAuthentication();

        app.UseEndpoints(e =>
        {
            e.MapControllerRoute(name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
