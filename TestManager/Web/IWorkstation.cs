namespace TestManager.Web;

public interface IWorkstation : TestEngineering.Interfaces.IWorkstation
{ 
    public Task SyncWorkstation();
}
