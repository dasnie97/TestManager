namespace TestManager.Features.Transporters;

public class NoFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;
    public NoFilesTransporter()
    {

    }

    public Task TransportTestReports()
    {
        // No files are transported
        return Task.CompletedTask;
    }
}
