using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TestManager.Features.ProductionSupervision;
using TestManager.Configuration;
using TestManager.Web;
using TestManager.Features.Transporters;
using TestManager.FileManagement;
using TestEngineering.Interfaces;
using Microsoft.Extensions.Configuration;
using TestManager.Features.TrackedTestReports;
using TestEngineering.Web;

namespace TestManager;

internal static class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    [STAThread]
    static void Main()
    {
        try
        {
            Application.ThreadException += Application_ThreadException;
            ApplicationConfiguration.Initialize();
            var host = CreateHostBuilder().Build();

            ServiceProvider = host.Services;
            Log.Logger.Information("Starting up application...");
            Application.Run(ServiceProvider.GetRequiredService<MainForm>());

            Log.Logger.Information("Shutting down application...");
            host.Dispose();
            Log.CloseAndFlush();
        }
        catch (Exception ex)
        {
            var exceptionMessage = $"Exception occured at {ex.Source}.\n{ex.Message}";
            MessageBox.Show(exceptionMessage);
            Log.Logger.Error(ex, ex.ToString());
            Environment.Exit(Environment.ExitCode);
        }
    }

    private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
    {
        var exceptionMessage = $"Exception occured at {e.Exception.Source}.\n{e.Exception.Message}";
        MessageBox.Show(exceptionMessage);
        Log.Logger.Error(e.Exception, e.Exception.ToString());
        Environment.Exit(Environment.ExitCode);
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
                services.AddSingleton<IWorkstationFactory, WorkstationFactory>();
                services.AddSingleton<IStatistics, Statistics>();
                services.AddSingleton<IWebAdapter, WebAdapter>();
                services.AddSingleton<IFTPService, FTPService>();
                services.AddSingleton<IHTTPService, HTTPService>();
                services.AddSingleton<IFileProcessor, FileProcessor>();
                services.AddSingleton<ITransporterFactory, TransporterFactory>();
                services.AddSingleton<IProblemDetector, ProblemDetector>();
                services.AddSingleton<ITestReportTracker, TestReportTracker>();
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