using ProductTest.Models;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class PassedFilesTransporter : ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private IEnumerable<FileTestReport> _fileTestReports;

    public PassedFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
    }

    public void TransportTestReports()
    {
        _fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in _fileTestReports)
        {
            if (file.Status == TestStatus.Passed)
            {
                ProcessFile(file);
            }
            else
            {
                RemoveFile(file);
            }
        }

        _fileProcessor.Reset();
    }

    private void ProcessFile(FileTestReport file)
    {
        _fileProcessor.CopyFile(file);
        _fileProcessor.MoveFile(file);
        _statistics.Add(new TrackedTestReport(file));
    }

    private void RemoveFile(FileTestReport file)
    {
        _fileProcessor.DeleteFile(file);
    }
}
