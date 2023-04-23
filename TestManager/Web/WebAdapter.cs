using TestEngineering.DTO;
using TestEngineering.Models;
using TestManager.Configuration;
using TestManager.Features.Analysis;
using TestManager.Web.Converters;

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
            Task<HttpContent> task = _httpService.HttpPostTestReport(dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public Task HTTPUpload(RemoteWorkstation workstation)
    {
        var dto = CreateDTO(workstation);

        if (_webConfig.SendOverHTTP)
        {
            Task<HttpContent> task = _httpService.HttpPostTestReport(dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public TestReportDTO HTTPGetTestReport(string serialNumber)
    {
        var response = _httpService.HttpGetTestReport<TestReportDTO>(serialNumber);
        var foundData = response.Result;

        var ordered = foundData.OrderByDescending(t=>t.RecordCreated);
        return ordered.FirstOrDefault();
    }

    public Task<List<WorkstationDTO>> HTTPGetWorkstation(string name)
    {
        Task<List<WorkstationDTO>> task = _httpService.HttpGetWorkstation<WorkstationDTO>(name);
        return task;
    }

    public Task HTTPPutWorkstation(WorkstationDTO workstation)
    {
        Task task = _httpService.HttpPutWorkstation(workstation);
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
