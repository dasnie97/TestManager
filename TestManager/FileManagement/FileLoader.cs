﻿using TestEngineering.Models;
using System.Text.RegularExpressions;

namespace TestManager.FileManagement;

public class FileLoader : IFileLoader
{
    public IEnumerable<string> GetFiles(string inputDirectoryPath)
    {
        Regex r = new Regex(@"\d{8}\w{1}\d{6}\w{1}");

        //////////////////////////////////////////////////////////////////////////////////
        //File name examples:    01032022_163836_20172797560108.txt
        //                      01052022_213920_22023520891916E.txt
        //                      01072022_123751_HID=582240701D7P2021481001056&REV=K.txt
        //////////////////////////////////////////////////////////////////////////////////

        List<string> files = Directory.GetFiles(inputDirectoryPath, "*.txt")
                 .Where(path => r.IsMatch(path) && new FileInfo(path).Length != 0)
                 .ToList();

        WaitForFilesUnlocked(files);
        return files;
    }

    public IEnumerable<FileTestReport> GetTestReportFiles(string inputDirectoryPath)
    {
        var loadedFiles = GetFiles(inputDirectoryPath);
        return loadedFiles.Select(file => FileTestReport.CreateFromFile(file));
    }

    private void WaitForFilesUnlocked(IEnumerable<string> files)
    {
        foreach (var file in files)
        {
            for (int i = 0; i < 5; i++)
            {
                if (!IsFileLocked(new FileInfo(file)))
                    break;
                Thread.Sleep(100);
            }
        }
    }

    private bool IsFileLocked(FileInfo file)
    {
        try
        {
            using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
            {
                stream.Close();
            }
        }
        catch (IOException)
        {
            return true;
        }
        return false;
    }
}
