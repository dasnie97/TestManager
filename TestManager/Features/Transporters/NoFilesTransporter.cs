namespace TestManager.Features.Transporters;

public class NoFilesTransporter : ITransporter
{
    public NoFilesTransporter()
    {

    }

    public event EventHandler FileTransported;

    public void TransportTestReports()
    {
        // No files are transported
    }
}
