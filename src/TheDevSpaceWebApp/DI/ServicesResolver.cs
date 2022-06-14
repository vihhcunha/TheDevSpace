using TheDevSpace.Application;
using TheDevSpace.Application.ValidationService;
using TheDevSpace.Repository;
using TheDevSpace.Repository.Repository;
using TheDevSpaceWebApp.Services;

namespace TheDevSpaceWebApp.DI;

public static class ServicesResolver
{
    public static void ResolveServices(this IServiceCollection services)
    {
        // Repository
        services.AddScoped<TheDevSpaceContext>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWriterRepository, WriterRepository>();

        // Application
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWriterService, WriterService>();
        services.AddScoped<IValidationService, ValidationService>();

        // Web
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    }
}
