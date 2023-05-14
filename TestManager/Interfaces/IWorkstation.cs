namespace TestManager.Interfaces;

public interface IWorkstation : TestEngineering.Interfaces.IWorkstation
{
    public Task SyncWorkstation();
}
