using Microsoft.Extensions.Configuration;

namespace TestManager.Configuration;

public class Config : IDirectoryConfig, IWorkstationConfig, IWebConfig
{
    public string TestStationName { get; set; }
    public bool SendOverFTP { get; set; }
    public bool SendOverHTTP { get; set; }
    public bool VerifyMES { get; set; }
    public bool Verify3510 { get; set; }
    public string InputDir { get; set; }
    public string OutputDir { get; set; }
    public string CopyDir { get; set; }
    public string DateNamedCopyDirectory { get; set; }
    public bool IsCopyingEnabled { get; set; } = false;

    public Config()
    {
        ReadConfig();
        CreateDateNamedCopyDir();
    }

    private void ReadConfig()
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        IConfigurationSection section = config.GetSection(this.GetType().Name);
        section.Bind(this);
    }

    private void CreateDateNamedCopyDir()
    {
        if (Directory.Exists(CopyDir))
        {
            IsCopyingEnabled = true;
            var dateNamedDirectory = Path.Combine(CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
            Directory.CreateDirectory(dateNamedDirectory);
            DateNamedCopyDirectory = dateNamedDirectory;
        }
    }
}
