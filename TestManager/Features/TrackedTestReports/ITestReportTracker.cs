using TestEngineering.Models;

namespace TestManager.Features.TrackedTestReports
{
    public interface ITestReportTracker
    {
        ITrackedTestReport CreateTrackedTestReport(TestReport testReport);
    }
}