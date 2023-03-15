using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TestManager.Features.ProductionSupervision;
using TestManager.Configuration;
using TestManager.Web;
using TestManager.Features.Transporters;
using TestManager.FileManagement;
using ProductTest.Interfaces;
using Microsoft.Extensions.Configuration;

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

            Log.Logger.Information("Shutting down application...");
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
            .ConfigureAppConfiguration(builder =>
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Prodction"}.json", optional: true);
            })
            .UseSerilog((hostingContext, services, loggerConfiguration) =>
                BuildLogger(loggerConfiguration, hostingContext)
            )
            .ConfigureServices((context, services) =>
            {
                services.ConfigureWritable<Config>(context.Configuration.GetSection(nameof(Config)));
                services.AddSingleton<IDirectoryConfig, Config>();
                services.AddSingleton<IWebConfig, Config>();
                services.AddSingleton<IWorkstationConfig, Config>();
                services.AddSingleton<IWorkstation, Workstation>();
                services.AddSingleton<IStatistics, Statistics>();
                services.AddSingleton<IWebAdapter, WebAdapter>();
                services.AddSingleton<IFTPService, FTPService>();
                services.AddSingleton<IFileProcessor, FileProcessor>();
                services.AddSingleton<ITransporterFactory, TransporterFactory>();
                services.AddSingleton<MainForm>();
            });
    }

    private static void BuildLogger(LoggerConfiguration loggerConfiguration, HostBuilderContext hostingContext)
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day);
    }
}