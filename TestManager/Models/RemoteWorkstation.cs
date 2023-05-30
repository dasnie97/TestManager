using TestEngineering.DTO;
using TestEngineering.Models;
using TestManager.Configuration;
using TestManager.Interfaces;

namespace TestManager.Models;

public class RemoteWorkstation : IWorkstation
{
    public string Name { get; set; }
    public string OperatorName { get; set; }
    public string ProcessStep { get; set; }
    public WorkstationState State { get; set; }

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
            await _webAdapter.HTTPPost(this);
        }
        else
        {
            State = WorkstationState.Running;
            await _webAdapter.HTTPPutWorkstation(this);
        }
    }
}
