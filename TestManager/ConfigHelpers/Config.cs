using System.Configuration;

namespace TestManager.ConfigHelpers;

public sealed class Config : IDirectoryConfig, IWorkstationConfig, IWebConfig
{
    public string TestStationName { get; private set; }
    public bool SendOverFTP { get; private set; }
    public bool SendOverHTTP { get; private set; }
    public bool VerifyMES { get; private set; }
    public bool Verify3510 { get; private set; }
    public string InputDir { get; private set; }
    public string OutputDir { get; private set; }
    public string CopyDir { get; private set; }
    public string DateNamedCopyDirectory { get; private set; }
    public bool IsCopyingEnabled { get; private set; } = false;

    private Configuration ConfigFile;
    private Config(Configuration configuration)
    {
        ConfigFile = configuration;
        ReadConfig();
        CreateDateNamedCopyDir();
    }

    public static Config GetInstance()
    {
        var defaultConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        return new Lazy<Config>(() => new Config(defaultConfiguration)).Value;
    }

    public static Config GetInstance(Configuration configuration)
    {
        return new Lazy<Config>(() => new Config(configuration)).Value;
    }

    public void WriteConfig(string key, string value)
    {
        ConfigFile.AppSettings.Settings[key].Value = value;
        ConfigFile.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(ConfigFile.AppSettings.SectionInformation.Name);
        ReadConfig();
        CreateDateNamedCopyDir();
    }

    private void ReadConfig()
    {
        var _cfg = ConfigFile.AppSettings.Settings;
        TestStationName = _cfg["TestStationName"].Value ?? "Not found";
        SendOverFTP = Convert.ToBoolean(_cfg["SendOverFTP"].Value);
        SendOverHTTP = Convert.ToBoolean(_cfg["SendOverHTTP"].Value);
        VerifyMES = Convert.ToBoolean(_cfg["VerifyMES"].Value);
        Verify3510 = Convert.ToBoolean(_cfg["Verify3510"].Value);
        InputDir = _cfg["InputDir"].Value;
        OutputDir = _cfg["OutputDir"].Value;
        CopyDir = _cfg["CopyDir"].Value;
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
