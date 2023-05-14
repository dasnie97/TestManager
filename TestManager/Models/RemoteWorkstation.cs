using TestEngineering.DTO;
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
        WorkstationDTO remoteWorkstation = task.Result.FirstOrDefault();
        remoteWorkstation.ProcessStep = ProcessStep;

        var updateStatus = _webAdapter.HTTPPutWorkstation(remoteWorkstation);
    }
}
