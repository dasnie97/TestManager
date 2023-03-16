namespace TestManager.Features.Transporters;

public class NoFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;
    public NoFilesTransporter()
    {

    }

    public void TransportTestReports()
    {
        // No files are transported
    }
}
