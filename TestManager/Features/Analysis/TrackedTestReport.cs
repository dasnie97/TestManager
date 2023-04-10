using ProductTest.Models;

namespace TestManager.Features.Analysis;

public class TrackedTestReport : TestReport
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }

    public TrackedTestReport(TestReport testReportBase)
    {
        SerialNumber = testReportBase.SerialNumber;
        Workstation = testReportBase.Workstation;
        TestSteps = testReportBase.TestSteps;
        TestDateTimeStarted = testReportBase.TestDateTimeStarted;
        Status = testReportBase.Status;
        Failure = testReportBase.Failure;
        FixtureSocket = testReportBase.FixtureSocket;
        TestingTime = testReportBase.TestingTime;
        IsFirstPass = true;
        IsFalseCall = false;
    }
}
