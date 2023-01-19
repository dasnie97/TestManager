using ProductTest.Common;
using ProductTest.Models;
using TestManager.Interfaces;

namespace TestManager.Helpers;

public class FileProcessor
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; } = 0;
    public List<TrackedTestReport> ProcessedData { get; set; } = new List<TrackedTestReport>();
    public FileProcessor(ConfigHandler configLoader, Statistics statistics)
    {
        _cfg = configLoader;
        _statistics = statistics;
    }
    private IFileLoader _fileLoader = new FileLoader();
    private readonly Statistics _statistics;
    protected readonly ConfigHandler _cfg;        
    private Workstation workstation;

    public void ProcessFiles()
    {
        if (IsDataTransferEnabled)
        {
            var testReports = _fileLoader.GetTestReportFiles(_cfg.InputDir);
            if (testReports.Any())
            {
                TransferFiles(testReports);
            }
        }
    }

    protected virtual async Task TransferFiles(IEnumerable<FileTestReport> testReports)
    {
        var filteredTestReports = FilterFilesBasingOnTransferringOption(testReports);
        if (_cfg.CopyDir != String.Empty)
        {
            CopyFiles(filteredTestReports);
        }
        UpdateStatistics(filteredTestReports);
        MoveFiles(filteredTestReports);
        TransferOption = -1;
    }

    protected IEnumerable<FileTestReport> FilterFilesBasingOnTransferringOption(IEnumerable<FileTestReport> testReports)
    {
        switch (TransferOption)
        {
            case -1:
                return testReports;
            case 0:
                testReports.Where(testReport => testReport.Status != TestStatus.Passed).ToList().
                    ForEach(testReport => File.Delete(testReport.FilePath));
                return testReports.Where(testReport => testReport.Status == TestStatus.Passed);
            case 1:
                testReports.ToList().ForEach(testReport => File.Delete(testReport.FilePath));
                return new List<FileTestReport>();
            case 2:
                return testReports;
            default:
                throw new Exception("File transfer option has invalid value!");
        }
    }

    protected void UpdateStatistics(IEnumerable<TestReportBase> testReports)
    {
        foreach (TestReportBase testReport in testReports)
        {
            var trackedTestReport = new TrackedTestReport(testReport);
            ProcessedData.Add(trackedTestReport);
            _statistics.numberOfFilesProcessed++;
            if (testReport.Status != TestStatus.Passed)
                _statistics.numberOfFilesFailed++;
        }
    }

    protected void CopyFiles(IEnumerable<FileTestReport> testReports)
    {
        var cdir = Path.Combine(_cfg.CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
        Directory.CreateDirectory(cdir);
        testReports.ToList().ForEach(testReport => 
            File.Copy(testReport.FilePath, Path.Combine(cdir, Path.GetFileName(testReport.FilePath))));
    }

    protected void MoveFiles(IEnumerable<FileTestReport> testReports)
    {
        testReports.ToList().ForEach(testReport => 
            File.Move(testReport.FilePath, Path.Combine(_cfg.OutputDir, Path.GetFileName(testReport.FilePath)), true));
    }
}
