using ProductTest.Interfaces;
using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;
using TestManager.Transporters;

namespace TestManager.Other;

public class CustomTransporter : TransporterBase
{
    protected override IEnumerable<FileTestReport> LoadTestReports()
    {
        IFileLoader fileLoader = new FileLoader();
        IEnumerable<FileTestReport> loaded = fileLoader.GetTestReportFiles(Config.Instance.InputDir);
        List<FileTestReport> modified = new List<FileTestReport>();

        foreach (var testReport in loaded)
        {
            var testStepsListed = testReport.TestSteps.ToList();

            foreach (var testStep in testStepsListed)
            {
                if (testStep.Name == "3.1 Setup_iot:psk:remote:encryptedsecret")
                {
                    var index = testStepsListed.IndexOf(testStep);
                    var newTestStep = TestStep.Create("iot:psk:remote:encryptedsecret",
                                                        testStep.DateTimeFinish,
                                                        testStep.Status,
                                                        testStep.Type,
                                                        testStep.Value,
                                                        testStep.Unit,
                                                        testStep.LowerLimit,
                                                        testStep.UpperLimit,
                                                        testStep.Failure);

                    testStepsListed[index] = newTestStep;
                    break;
                }
            }
            var newTestReport = FileTestReport.Create(testReport.SerialNumber, testReport.Workstation.Name, testStepsListed, testReport.FilePath);
            //TODO: creating new files may change file name leading to exceptions. Solve this by reading file name and content, replace content and save
            // with same name.
            newTestReport.SaveReport(Config.Instance.InputDir);
            modified.Add(newTestReport);
        }
        return modified;
    }
}
