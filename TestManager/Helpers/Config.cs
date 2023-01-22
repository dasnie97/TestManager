using System.Configuration;
using TestManager.Common;

namespace TestManager.Helpers;

public class Config : ConfigSettings
{
    public string DateNamedCopyDirectory { get; private set; }
    public bool IsCopyingEnabled { get; private set; } = false;
    public Configuration ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    public Config()
    {
        ReadConfig();
        Setup();
    }

    public void WriteConfig(string key, string value)
    {
        ConfigFile.AppSettings.Settings[key].Value = value;
        ConfigFile.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection(ConfigFile.AppSettings.SectionInformation.Name);
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

    private void Setup()
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
