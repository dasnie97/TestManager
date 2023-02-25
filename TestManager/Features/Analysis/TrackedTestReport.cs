using ProductTest.Common;

namespace TestManager.Features.Analysis;

public class TrackedTestReport : TestReportBase
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }

    public TrackedTestReport(TestReportBase testReportBase)
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
