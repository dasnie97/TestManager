using TestManager.Configuration;

namespace TestManager.Web;

public class WorkstationFactory : IWorkstationFactory
{
    private readonly IWebConfig _webConfig;
    private readonly IWorkstationConfig _workstationConfig;
    private readonly IWebAdapter _webAdapter;

    public WorkstationFactory(IWebConfig webConfig, IWorkstationConfig workstationConfig, IWebAdapter webAdapter)
    {
        _webConfig = webConfig;
        _workstationConfig = workstationConfig;
        _webAdapter = webAdapter;
    }

    public IWorkstation CreateWorkstation()
    {
        if (_webConfig.SendOverHTTP)
        {
            return new RemoteWorkstation(_workstationConfig, _webAdapter);
        }
        else
        {
            return new LocalWorkstation(_workstationConfig);
        }
    }
}
