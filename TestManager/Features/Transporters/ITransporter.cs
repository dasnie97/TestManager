namespace TestManager.Features.Transporters;

public interface ITransporter
{
    public Task TransportTestReports();
    public event EventHandler FileTransported;
}
