﻿using TestEngineering.Models;
using TestManager.Interfaces;
using TestManager.Models;

namespace TestManager.Services;

public class ProblemDetector : IProblemDetector
{
    private readonly IStatistics _statistics;
    public ProblemDetector(IStatistics statistics)
    {
        _statistics = statistics;
    }

    public void RunDetector()
    {
        //TODO: How test reports are classified as false calls or first passed?
        //TODO: Program ProblemDetector logic
        // If 3510 rule is violated - dont allow operator to continue working and PUT workstation status
        // If test data is not showing in MES - turn off data transfer, and PUT workstation status
    }

    private ITrackedTestReport CheckFor3510RuleViolation(ITrackedTestReport LF)
    {
        var sameSNs = _statistics.GetProcessedData().Where(logFile => logFile.SerialNumber == LF.SerialNumber);
        if (sameSNs.Any()) LF.IsFirstPass = false;
        else LF.IsFirstPass = true;

        var failedSameSNs = sameSNs.Where(logFile => logFile.Status == TestStatus.Failed && logFile.TestDateTimeStarted >= DateTime.Now.AddMinutes(-15));
        if (failedSameSNs.Any() && LF.Status == TestStatus.Passed) LF.IsFalseCall = true;
        else LF.IsFalseCall = false;

        var firstPass = _statistics.GetProcessedData().
            Where((logFile) => logFile.IsFirstPass == true);

        var rule3Check = firstPass.Skip(Math.Max(0, firstPass.Count() - 3));

        if (rule3Check.Where(logFile => logFile.Status == TestStatus.Failed).Count() >= 3)
        {
            // 3 violation
        }

        var rule5Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= DateTime.Now.AddHours(-1));

        if (rule5Check.Where(logFile => logFile.Status == TestStatus.Failed).Count() >= 5)
        {
            // 5 violation
        }

        var rule10Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= ProductionShift.CurrentShift.ShiftStart && logFile.TestDateTimeStarted < ProductionShift.CurrentShift.ShiftEnd);

        if (rule10Check.Where(logFile => logFile.Status == TestStatus.Failed).Count() >= 10)
        {
            // 10 violation
        }

        return LF;
    }

    private async Task<string> Check3510(string state)
    {
        var firstPassData = _statistics.GetProcessedData().Where(logFile => logFile.IsFirstPass);
        if (firstPassData.Count() < 3) return state;

        for (int i = firstPassData.Count(); i > 0; i--)
        {
            firstPassData.ElementAt(Index.FromStart(i));
        }

        return state;
    }
}
