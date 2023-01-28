﻿using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;

namespace TestManager.Transporters;

public class CustomTransporter : TransporterBase
{
    protected override IEnumerable<FileTestReport> LoadTestReports()
    {
        IFileLoader fileLoader = new FileLoader();
        IDirectoryConfig config = Config.GetInstance(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(config.InputDir);

        foreach (var testReport in loaded)
        {
            ReplaceFileContent(testReport.FilePath);
        }

        return fileLoader.GetTestReportFiles(config.InputDir);
    }

    private void ReplaceFileContent(string filePath)
    {
        Regex regex = new Regex("TestName.*?iot:psk:remote:encryptedsecret");
        var originalFileContent = File.ReadAllText(filePath);
        var newFileContent = regex.Replace(originalFileContent, "TestName:\tiot:psk:remote:encryptedsecret");
        File.WriteAllText(filePath, newFileContent);
    }
}