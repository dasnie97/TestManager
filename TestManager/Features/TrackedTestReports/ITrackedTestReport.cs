using ProductTest.Models;

namespace TestManager.Features.TrackedTestReports
{
    public interface ITrackedTestReport
    {
        Workstation Workstation { get; }
        string SerialNumber { get; }
        TestStatus Status { get; }
        DateTime TestDateTimeStarted { get; }
        IEnumerable<TestStep> TestSteps { get; }
        TimeSpan TestingTime { get; }
        string FixtureSocket { get; }
        string Failure { get; }
        bool IsFalseCall { get; set; }
        bool IsFirstPass { get; set; }
    }
}