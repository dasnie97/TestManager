public interface IAppSettingsWriter
{
    void UpdateAppSetting(string sectionName, string key, bool value);
}