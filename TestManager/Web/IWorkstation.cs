namespace TestManager.Web;

public interface IWorkstation
{
    public string Name { get; }
    public string OperatorName { get; }
    public string ProcessStep { get; }
    public Task SyncWorkstation();
}
