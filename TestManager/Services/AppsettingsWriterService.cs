using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

public class AppSettingsWriterService : IAppSettingsWriter
{
    private readonly IConfigurationRoot _configurationRoot;
    private readonly string _appSettingsFilePath;

    public AppSettingsWriterService(IConfiguration configuration)
    {
        _appSettingsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        _configurationRoot = (IConfigurationRoot)configuration;
    }

    public void UpdateAppSetting(string sectionName, string key, bool value)
    {
        JObject appSettingsJson = JObject.Parse(File.ReadAllText(_appSettingsFilePath));
        var appSettingsSection = appSettingsJson.GetValue(sectionName) as JObject;

        if (appSettingsSection != null)
        {
            appSettingsSection[key] = value;

            File.WriteAllText(_appSettingsFilePath, appSettingsJson.ToString());
            _configurationRoot.Reload();
        }
        else
        {
            throw new Exception($"Section '{sectionName}' not found in appsettings.json.");
        }
    }
}
