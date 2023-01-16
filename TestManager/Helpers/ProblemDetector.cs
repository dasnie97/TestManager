﻿using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public class ProblemDetector
{
    private readonly FileProcessor _fileProcessor;
    public ProblemDetector(FileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }

    /// <summary>
    /// Checks if production problem exists analyzing processed data. If true - communicates it.
    /// </summary>
    public void Run()
    {
        if (ThereIsNoTraffic())
        {
            return;
        }

        //TODO: Program ProblemDetector logic
        // If 3510 rule is violated - dont allow operator to continue working and PUT workstation status
        // If test data is not showing in MES - turn off data transfer, and PUT workstation status
    }

    private bool ThereIsNoTraffic()
    {
        var latestResults = _fileProcessor.ProcessedData.Where(logFile => logFile.TestDateTimeStarted >= DateTime.Now.AddMinutes(-20));
        if (latestResults.Count() == 0) return true;
        else return false;
    }

    private TrackedTestReport CheckFor3510RuleViolation(TrackedTestReport LF)
    {
        var sameSNs = _fileProcessor.ProcessedData.Where(logFile => logFile.SerialNumber == LF.SerialNumber);
        if (sameSNs.Any()) LF.IsFirstPass = false;
        else LF.IsFirstPass = true;

        var failedSameSNs = sameSNs.Where(logFile => logFile.Status == "Failed" && logFile.TestDateTimeStarted >= DateTime.Now.AddMinutes(-15));
        if (failedSameSNs.Any() && LF.Status == "Passed") LF.IsFalseCall = true;
        else LF.IsFalseCall = false;

        var firstPass = _fileProcessor.ProcessedData.
            Where((logFile) => logFile.IsFirstPass == true);

        var rule3Check = firstPass.Skip(Math.Max(0, firstPass.Count() - 3));

        if (rule3Check.Where(logFile => logFile.Status == "Failed").Count() >= 3)
        {
            // 3 violation
        }

        var rule5Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= DateTime.Now.AddHours(-1));

        if (rule5Check.Where(logFile => logFile.Status == "Failed").Count() >= 5)
        {
            // 5 violation
        }

        var rule10Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= ProductionShift.CurrentShift.ShiftStart && logFile.TestDateTimeStarted < ProductionShift.CurrentShift.ShiftEnd);

        if (rule10Check.Where(logFile => logFile.Status == "Failed").Count() >= 10)
        {
            // 10 violation
        }

        return LF;
    }

    private async Task<string> Check3510(string state)
    {
        var firstPassData = _fileProcessor.ProcessedData.Where(logFile => logFile.IsFirstPass);
        if (firstPassData.Count() < 3) return state;

        for (int i = firstPassData.Count(); i > 0; i--)
        {
            firstPassData.ElementAt(Index.FromStart(i));
        }

        return state;
    }
}
