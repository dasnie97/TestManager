namespace TestManager.Configuration;

public interface IWorkstationConfig
{
    public string TestStationName { get; }
    public string OperatorName { get; }
    public string ProcessStep { get; }
    public void ReadConfig();
}
