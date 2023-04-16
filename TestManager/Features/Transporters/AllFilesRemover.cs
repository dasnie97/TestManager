using TestManager.FileManagement;

namespace TestManager.Features.Transporters;

public class AllFilesRemover : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    public AllFilesRemover(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }

    public Task TransportTestReports()
    {
        var fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in fileTestReports)
        {
            _fileProcessor.DeleteFile(file);
        }

        return Task.CompletedTask;
    }
}
