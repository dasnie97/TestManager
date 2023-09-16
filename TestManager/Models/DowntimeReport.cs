﻿using TestEngineering.Interfaces;
using TestEngineering.Models;

namespace TestManager.Models;

public class DowntimeReport
{
    public string ProblemDescription { get; set; }
    public string ActionTaken { get; set; }
    public string Technician { get; set; }
    public string Workstation { get; set; }
    public string Operator { get; set; }
    public DateTime TimeStarted { get; set; }
    public DateTime TimeFinished { get; set; }
    public TimeSpan TotalDowntime { get; set; }
}
