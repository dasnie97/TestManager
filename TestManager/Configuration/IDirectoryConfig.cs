namespace TestManager.Configuration;

public interface IDirectoryConfig
{
    public string InputDir { get; set; }
    public string OutputDir { get; set; }
    public string CopyDir { get; set; }
    public bool IsCopyingEnabled { get; set; }
    public string DateNamedCopyDirectory { get; set; }
}