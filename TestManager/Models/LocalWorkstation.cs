using TestManager.Configuration;
using TestManager.Interfaces;

namespace TestManager.Models;

public class LocalWorkstation : IWorkstation
{
    public string Name { get; }
    public string OperatorName { get; }
    public string ProcessStep { get; }

    private readonly IWorkstationConfig _workstationConfig;

    public LocalWorkstation(IWorkstationConfig workstationConfig)
    {
        _workstationConfig = workstationConfig;

        Name = _workstationConfig.TestStationName;
        OperatorName = _workstationConfig.OperatorName;
        ProcessStep = _workstationConfig.ProcessStep;
    }

    public Task SyncWorkstation()
    {
        return Task.CompletedTask;
    }
}
