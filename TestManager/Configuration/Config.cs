using Microsoft.Extensions.Configuration;

namespace TestManager.Configuration;

public class Config : IWorkstationConfig, IWebConfig, IDirectoryConfig
{
    public string TestStationName { get; set; }
    public string OperatorName { get; set; }
    public bool SendOverFTP { get; set; }
    public bool SendOverHTTP { get; set; }
    public bool VerifyMES { get; set; }
    public bool Verify3510 { get; set; }
    public string InputDir { get; set; }
    public string OutputDir { get; set; }
    public string CopyDir { get; set; }
    public string DateNamedCopyDirectory { get; set; }
    public bool IsCopyingEnabled { get; set; } = false;


    private readonly IConfiguration _configuration;

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
        ReadConfig();
        CreateDateNamedCopyDir();
    }

    public Config()
    {
        
    }

    public void ReadConfig()
    {
        IConfigurationSection section = _configuration.GetSection(this.GetType().Name);
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
