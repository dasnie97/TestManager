namespace TestManager.Common;

public abstract class ConfigSettings
{
    public string TestStationName { get; protected set; }
    public bool SendOverFTP { get; protected set; }
    public bool SendOverHTTP { get; protected set; }
    public bool VerifyMES { get; protected set; }
    public bool Verify3510 { get; protected set; }
    public string InputDir { get; protected set; }
    public string OutputDir { get; protected set; }
    public string CopyDir { get; protected set; }
}
