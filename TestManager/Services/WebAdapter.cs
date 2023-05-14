using TestEngineering.DTO;
using TestEngineering.Interfaces;
using TestEngineering.Models;
using TestManager.Interfaces;
using TestManager.Models;
using TestManager.Configuration;

namespace TestManager.Services;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IFTP _ftpService;
    private readonly IHTTP _httpService;

    public WebAdapter(IWebConfig webConfig,
        IFTP ftpService,
        IHTTP httpService)
    {
        _webConfig = webConfig;
        _ftpService = ftpService;
        _httpService = httpService;
    }

    public void FTPUpload(string filePath)
    {
        if (_webConfig.SendToFTP)
        {
            _ftpService.Upload(filePath);
        }
    }

    public Task HTTPUpload(TestReport testReport)
    {
        if (_webConfig.SendToWebAPI)
        {
            var dto = CreateDTO(testReport);
            Task<CreateTestReportDTO> task = _httpService.PostAsync("api/TestReport", dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public Task HTTPUpload(RemoteWorkstation workstation)
    {
        if (_webConfig.SendToWebAPI)
        {
            var dto = CreateDTO(workstation);
            Task<CreateWorkstationDTO> task = _httpService.PostAsync("api/Workstation", dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public Task<List<TestReportDTO>> HTTPGetTestReportsBySerialNumber(string serialNumber)
    {
        var parameters = new Dictionary<string, string>
        {
            { "serialNumber", serialNumber }
        };

        Task<List<TestReportDTO>> response = _httpService.GetAsync<TestReportDTO>("api/TestReport", parameters);
        return response;
    }

    public Task<List<WorkstationDTO>> HTTPGetWorkstationsByName(string name)
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
