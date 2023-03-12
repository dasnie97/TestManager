namespace TestManager.Features.Transporters;

public interface ITransporter
{
    public void TransportTestReports();
    public event EventHandler FileTransported;
}
