using Microsoft.EntityFrameworkCore;
using TheDevSpace.Repository;

namespace TheDevSpaceWebApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("TheDevSpaceConnectionString");

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
