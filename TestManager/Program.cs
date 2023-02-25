using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using TestManager.ConfigHelpers;
using TestManager.Features.ProductionSupervision;

namespace TestManager;

internal static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    [STAThread]
    static void Main()
    {
        try
        {
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;
            Log.Logger.Information("Starting up application...");
            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
            Log.Logger.Information("Closing application...");
            Log.CloseAndFlush();
            host.Dispose();
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, ex.ToString());
            Environment.Exit(Environment.ExitCode);
        }
    }

    private static IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
        .UseSerilog((hostingContext, services, loggerConfiguration) =>
            BuildLogger(loggerConfiguration, hostingContext)
        )
        .ConfigureServices((context, services) =>
        {
            services.ConfigureWritable<Config>(context.Configuration.GetSection(nameof(Config)));
            services.AddSingleton<IStatistics, Statistics>();
            services.AddTransient<MainForm>();
        });
    }

    private static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Prodction"}.json", optional: true);
    }

    private static void BuildLogger(LoggerConfiguration loggerConfiguration, HostBuilderContext hostingContext)
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
    }
}