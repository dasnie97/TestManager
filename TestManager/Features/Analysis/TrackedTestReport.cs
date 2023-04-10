using ProductTest.Models;

namespace TestManager.Features.Analysis;

public class TrackedTestReport : TestReport
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }
    public string ProcessStep { get; set; }


    public TrackedTestReport(TestReport testReport)
    {
        SerialNumber = testReport.SerialNumber;
        Workstation = testReport.Workstation;
        TestSteps = testReport.TestSteps;
        TestDateTimeStarted = testReport.TestDateTimeStarted;
        Status = testReport.Status;
        Failure = testReport.Failure;
        FixtureSocket = testReport.FixtureSocket;
        TestingTime = testReport.TestingTime;
        IsFirstPass = true;
        IsFalseCall = false;
        ProcessStep = "";
    }
}
