using TestEngineering.Models;

namespace TestManager.Interfaces
{
    public interface ITestReportTracker
    {
        ITrackedTestReport CreateTrackedTestReport(TestReport testReport);
    }
}