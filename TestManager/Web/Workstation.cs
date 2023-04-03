using TestManager.Configuration;

namespace TestManager.Web;

public class Workstation : ProductTest.Models.Workstation
{
    private readonly IWorkstationConfig _workstationConfig;
    public Workstation(IWorkstationConfig workstationConfig)
    {
        _workstationConfig = workstationConfig;
        Name = _workstationConfig.TestStationName;
        OperatorName = _workstationConfig.OperatorName;
    }
}
