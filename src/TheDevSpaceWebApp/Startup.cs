using Microsoft.EntityFrameworkCore;
using TheDevSpace.Application.Mappings;
using TheDevSpace.Repository;
using TheDevSpaceWebApp.DI;
using TheDevSpace.Application;
using Microsoft.AspNetCore.Authentication.Cookies;
using TheDevSpaceWebApp.Middlewares;
using Serilog;
using Azure.Identity;
using Microsoft.AspNetCore.DataProtection;
using Azure.Storage.Blobs;

namespace TheDevSpaceWebApp;

public class Startup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
    {
        var connectionString = config.GetConnectionString("TheDevSpaceConnectionString");

        services.AddHttpContextAccessor();
        services.UseCustomHashPasswordBuilder()
            .UseArgon2<UserDto>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = config.GetConnectionString("Redis");
            options.InstanceName = "thedevspace";
        });

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

        if (env.IsProduction())
        {
            services.AddDataProtection()
                .PersistKeysToAzureBlobStorage(new Uri(config.GetValue<string>("BlobStorageDataProtectionUrl")));
        }

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.AccessDeniedPath = "/Auth/Login";
                opt.LoginPath = "/Auth/Login";
                opt.Cookie.Name = "TheDevSpaceAuth";
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
        app.UseSerilogRequestLogging();
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
