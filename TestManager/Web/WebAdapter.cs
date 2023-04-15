using ProductTest.DTO;
using ProductTest.Models;
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

    public void HTTPUpload(TestReport testReport)
    {
        var dto = CreateDTO(testReport);

        if (_webConfig.SendOverHTTP)
        {
            _httpService.HttpPost(dto);
        }
    }

    public TestReportDTO HTTPGet(string serialNumber)
    {
        var response = _httpService.HttpGet<TestReportDTO>(serialNumber);
        var foundData = response.Result;

        return foundData.FirstOrDefault();
    }

    private CreateTestReportDTO CreateDTO(TestReport file)
    {
        return DTOConverter.ToCreateTestReportDTO(file);
    }
}
