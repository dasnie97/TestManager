using Microsoft.Extensions.Configuration;
using TestManager.Configuration;

namespace TestManager.Helpers;

public class Config : IWorkstationConfig, IWebConfig, IDirectoryConfig
{
    public string TestStationName { get; set; }
    public string OperatorName { get; set; }
    public string ProcessStep { get; set; }
    public bool SendToFTP { get; set; }
    public bool SendToWebAPI { get; set; }
    public bool VerifyMES { get; set; }
    public bool Verify3510 { get; set; }
    public string InputDir { get; set; }
    public string OutputDir { get; set; }
    public string CopyDir { get; set; }

    private readonly IConfiguration _configuration;

    public Config(IConfiguration configuration)
    {
        _configuration = configuration;
        ReadConfig();
    }

    public Config()
    {

    }

    public void ReadConfig()
    {
        _configuration.Bind("WorkstationConfig", this);
        _configuration.Bind("WebConfig", this);
        _configuration.Bind("DirectoryConfig", this);
    }
}
