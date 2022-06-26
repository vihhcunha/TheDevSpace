using Serilog;
using Serilog.Events;
using TheDevSpaceWebApp;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console(LogEventLevel.Debug)
            .WriteTo.Sentry(opt =>
            {
                opt.Dsn = builder.Configuration.GetValue<string>("SentryKey");
                opt.MinimumEventLevel = LogEventLevel.Warning;
                opt.InitializeSdk = builder.Environment.IsProduction();
                opt.AttachStacktrace = true;
            })
            .CreateLogger();

try
{
    builder.Host.UseSerilog();

    var startup = new Startup();
    startup.ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

    var app = builder.Build();
    startup.Configure(app, builder.Environment);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
