using TestEngineering.DTO;
using TestEngineering.Models;
using TestEngineering.Web;
using TestManager.Configuration;

namespace TestManager.Web;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IFTPService _ftpService;
    private readonly IHTTPService _httpService;

    public WebAdapter(IWebConfig webConfig,
        IFTPService ftpService,
        IHTTPService httpService)
    {
        _webConfig = webConfig;
        _ftpService = ftpService;
        _httpService = httpService;
    }

    public void FTPUpload(string filePath)
    {
        if (_webConfig.SendOverFTP)
        {
            _ftpService.Upload(filePath);
        }
    }

    public Task HTTPUpload(TestReport testReport)
    {
        var dto = CreateDTO(testReport);

        if (_webConfig.SendOverHTTP)
        {
            Task<CreateTestReportDTO> task = _httpService.PostAsync("api/TestReport", dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public Task HTTPUpload(RemoteWorkstation workstation)
    {
        var dto = CreateDTO(workstation);

        if (_webConfig.SendOverHTTP)
        {
            Task<CreateWorkstationDTO> task = _httpService.PostAsync("api/Workstation", dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public TestReportDTO HTTPGetTestReport(string serialNumber)
    {
        var parameters = new Dictionary<string, string>
        {
            { "serialNumber", serialNumber }
        };

        var response = _httpService.GetAsync<TestReportDTO>("api/TestReport", parameters);
        var foundData = response.Result;
        var ordered = foundData.OrderByDescending(t=>t.RecordCreated);

        return ordered.FirstOrDefault();
    }

    public Task<List<WorkstationDTO>> HTTPGetWorkstations(string name)
    {
        var parameters = new Dictionary<string, string>
        {
            { "name", name }
        };

        Task<List<WorkstationDTO>> task = _httpService.GetAsync<WorkstationDTO>("api/Workstation", parameters);
        return task;
    }

    public Task HTTPPutWorkstation(WorkstationDTO workstation)
    {
        Task task = _httpService.PutAsync("api/Workstation", workstation);
        return task;
    }


    private CreateTestReportDTO CreateDTO(TestReport file)
    {
        return DTOConverter.ToCreateTestReportDTO(file);
    }

    private CreateWorkstationDTO CreateDTO(RemoteWorkstation file)
    {
        return DTOConverter.ToCreateWorkstationDTO(file);
    }
}
