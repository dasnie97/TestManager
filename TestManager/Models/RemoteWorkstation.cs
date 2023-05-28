using TestEngineering.DTO;
using TestEngineering.Exceptions;
using TestManager.Configuration;
using TestManager.Interfaces;

namespace TestManager.Models;

public class RemoteWorkstation : IWorkstation
{
    public string Name { get; }
    public string OperatorName { get; }
    public string ProcessStep { get; }

    private readonly IWorkstationConfig _workstationConfig;
    private readonly IWebAdapter _webAdapter;

    public RemoteWorkstation(IWorkstationConfig workstationConfig, IWebAdapter webAdapter)
    {
        _workstationConfig = workstationConfig;
        _webAdapter = webAdapter;

        Name = _workstationConfig.TestStationName;
        OperatorName = _workstationConfig.OperatorName;
        ProcessStep = _workstationConfig.ProcessStep;
    }


    public async Task SyncWorkstation()
    {
        Task<List<WorkstationDTO>> task = _webAdapter.HTTPGetWorkstationsByName(Name);
        await task;

        if (task.Result.Count == 0)
        {
            throw new WorkstationNotFoundException($"Workstation '{Name}' does not exist in data base!");
        }
        else
        {
            WorkstationDTO remoteWorkstation = task.Result.FirstOrDefault();
            remoteWorkstation.ProcessStep = ProcessStep;
            await _webAdapter.HTTPPutWorkstation(remoteWorkstation);
        }
    }
}
