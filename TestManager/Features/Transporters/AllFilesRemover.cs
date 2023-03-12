﻿using TestManager.FileManagement;

namespace TestManager.Features.Transporters;

public class AllFilesRemover : ITransporter
{
    private readonly IFileProcessor _fileProcessor;
    public AllFilesRemover(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }

    public event EventHandler FileTransported;

    public void TransportTestReports()
    {
        var fileTestReports = _fileProcessor.LoadFiles();

        foreach (var file in fileTestReports)
        {
            _fileProcessor.DeleteFile(file);
        }
    }
}
