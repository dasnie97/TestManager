using ProductTest.Models;

namespace TestManager.Features.TrackedTestReports
{
    public interface ITrackedTestReport
    {
        bool IsFalseCall { get; set; }
        bool IsFirstPass { get; set; }
        string Failure { get; }
        string FixtureSocket { get; }
        string SerialNumber { get; }
        TestStatus Status { get; }
        DateTime TestDateTimeStarted { get; }
        TimeSpan TestingTime { get; }
        IEnumerable<TestStep> TestSteps { get; }
        Workstation Workstation { get; }
    }
}