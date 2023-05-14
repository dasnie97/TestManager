namespace TestManager.Interfaces;

public interface ITransporter
{
    public Task TransportTestReports();
    public event EventHandler FileTransported;
}
