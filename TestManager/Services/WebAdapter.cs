using TestEngineering.DTO;
using TestEngineering.Interfaces;
using TestEngineering.Models;
using TestManager.Interfaces;
using TestManager.Models;
using TestManager.Configuration;
using AutoMapper;

namespace TestManager.Services;

public class WebAdapter : IWebAdapter
{
    private readonly IWebConfig _webConfig;
    private readonly IFTP _ftpService;
    private readonly IHTTP _httpService;
    private readonly IMapper _mapper;

    public WebAdapter(IWebConfig webConfig,
        IFTP ftpService,
        IHTTP httpService,
        IMapper mapper)
    {
        _webConfig = webConfig;
        _ftpService = ftpService;
        _httpService = httpService;
        _mapper = mapper;
    }

    public void FTPUpload(string filePath)
    {
        if (_webConfig.SendToFTP)
        {
            _ftpService.Upload(filePath);
        }
    }

    public Task HTTPPost(TestReport testReport)
    {
        if (_webConfig.SendToWebAPI)
        {
            var dto = _mapper.Map<CreateTestReportDTO>(testReport);
            Task<CreateTestReportDTO> task = _httpService.PostAsync("api/TestReport", dto);
            return task;
        }
        return Task.CompletedTask;
    }

    public Task HTTPPost(RemoteWorkstation workstation)
    {
        if (_webConfig.SendToWebAPI)
        {
            var dto = _mapper.Map<CreateWorkstationDTO>(workstation);
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

    public Task HTTPPutWorkstation(RemoteWorkstation workstation)
    {
        var workstationDTO = _mapper.Map<WorkstationDTO>(workstation);
        Task task = _httpService.PutAsync("api/Workstation", workstationDTO);
        return task;
    }
}
