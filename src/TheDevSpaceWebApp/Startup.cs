using Microsoft.EntityFrameworkCore;
using TheDevSpace.Application.Mappings;
using TheDevSpace.Repository;
using TheDevSpaceWebApp.DI;
using TheDevSpace.Application;

namespace TheDevSpaceWebApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("TheDevSpaceConnectionString");

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

        app.UseEndpoints(e =>
        {
            e.MapControllerRoute(name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
