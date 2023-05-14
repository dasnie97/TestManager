namespace TestManager.Configuration;

public interface IDirectoryConfig
{
    public string InputDir { get; set; }
    public string OutputDir { get; set; }
    public string CopyDir { get; set; }
    public void ReadConfig();
}