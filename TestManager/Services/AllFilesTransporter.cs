﻿using TestEngineering.Models;
using TestManager.Interfaces;
using TestEngineering.Interfaces;
using System.Net.Sockets;
using TestManager.Models;

namespace TestManager.Services;

public class AllFilesTransporter : ITransporter
{
    public event EventHandler FileTransported;

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;
    private readonly ITestReportTracker _tracker;

    public AllFilesTransporter(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter, ITestReportTracker tracker)
    {
        _fileProcessor = fileProcessor;
        _statistics = statistics;
        _webAdapter = webAdapter;
        _tracker = tracker;
    }

    public async Task TransportTestReports()
    {
        var fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in fileTestReports)
        {
            await ProcessFiles(file);
            FileTransported?.Invoke(this, EventArgs.Empty);
        }
    }

    private async Task ProcessFiles(FileTestReport file)
    {
        try
        {
            _webAdapter.FTPUpload(file.FilePath);
            var httpTask = _webAdapter.HTTPPost(file);
            _fileProcessor.CopyFile(file);
            _fileProcessor.MoveFile(file);

            await httpTask;
            ITrackedTestReport trackedReport = _tracker.CreateTrackedTestReport(file);
            _statistics.Add(trackedReport);
        }
        catch (Exception ex) when (ex is HttpRequestException || ex is SocketException)
        {
            var locallyTrackedTestReport = new LocallyTrackedTestReport(file, _statistics);
            _statistics.Add(locallyTrackedTestReport);
        }
    }
}
