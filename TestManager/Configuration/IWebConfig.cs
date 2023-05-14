namespace TestManager.Configuration;

public interface IWebConfig
{
    public bool SendToFTP { get; }
    public bool SendToWebAPI { get; }
    public bool VerifyMES { get; }
    public bool Verify3510 { get; }
    public void ReadConfig();
}
