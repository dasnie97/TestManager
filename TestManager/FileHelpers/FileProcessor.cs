﻿using ProductTest.Models;
using System.Configuration;
using TestManager.ConfigHelpers;
using TestManager.Helpers;

namespace TestManager.FileHelpers;

public class FileProcessor : IFileProcessor
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }
    private readonly IDirectoryConfig _config;
    public FileProcessor(IDirectoryConfig config)
    {
        _config = config;
    }

    public IEnumerable<FileTestReport> LoadFiles()
    {
        IFileLoader fileLoader = new FileLoader();
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(_config.InputDir);
        return loaded;
    }

    public string CopyFile(FileTestReport testReport)
    {
        var copiedFilePath = string.Empty;
        if (_config.IsCopyingEnabled)
        {
            var destinationFileName = Path.Combine(_config.DateNamedCopyDirectory, Path.GetFileName(testReport.FilePath));
            File.Copy(testReport.FilePath, destinationFileName);
            copiedFilePath = destinationFileName;
        }
        return copiedFilePath;
    }

    public string MoveFile(FileTestReport testReport)
    {
        var destinationFilePath = Path.Combine(_config.OutputDir, Path.GetFileName(testReport.FilePath));
        File.Move(testReport.FilePath, destinationFilePath, true);
        return destinationFilePath;
    }

    public void DeleteFile(FileTestReport testReport)
    {
        File.Delete(testReport.FilePath);
    }

    public void Reset()
    {
        IsDataTransferEnabled = true;
        TransferOption = 2;
    }
}
