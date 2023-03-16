namespace TestManager.Configuration;

public interface IWebConfig
{
    public bool SendOverFTP { get; }
    public bool SendOverHTTP { get; }
    public bool VerifyMES { get; }
    public bool Verify3510 { get; }
    public void ReadConfig();
}
