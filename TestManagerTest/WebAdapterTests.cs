using Moq;
using TestManager.Configuration;
using TestEngineering.DTO;
using TestEngineering.Other;
using TestEngineering.Interfaces;
using TestManager.Services;

namespace TestManagerTest;

public class WebAdapterTests
{
    private readonly WebAdapter adapter;
    private readonly Mock<IFTP> mockFtpService;
    private readonly Mock<IWebConfig> mockWebConfig;
    private readonly Mock<IHTTP> mockHTTPService;

    public WebAdapterTests()
    {
        mockFtpService = new Mock<IFTP>();
        mockWebConfig = new Mock<IWebConfig>();
        mockHTTPService = new Mock<IHTTP>();
        adapter = new WebAdapter(mockWebConfig.Object, mockFtpService.Object, mockHTTPService.Object);
    }

    [Fact]
    public void FTPUpload_ShouldCallFTPUploadWhenWebConfigOptionIsSetToUseFTP()
    {
        // Arrange
        string localFilePath = "testfile.txt";
        mockWebConfig.SetupGet(m=>m.SendOverFTP).Returns(true);

        // Act
        adapter.FTPUpload(localFilePath);

        // Assert
        mockFtpService.Verify(m => m.Upload(localFilePath), Times.Once);
    }

    [Fact]
    public void FTPUpload_ShouldNotCallFTPUploadWhenWebConfigOptionIsSetToNotUseFTP()
    {
        // Arrange
        string localFilePath = "testfile.txt";
        mockWebConfig.SetupGet(m => m.SendOverFTP).Returns(false);

        // Act
        adapter.FTPUpload(localFilePath);

        // Assert
        mockFtpService.Verify(m => m.Upload(localFilePath), Times.Never);
    }


    [Fact]
    public void HTTPUpload_ShouldCallHTTPPostWhenWebConfigOptionIsSetToUseHTTP()
    {
        mockWebConfig.SetupGet(m=>m.SendOverHTTP).Returns(true);
        var testReport = TestReportGenerator.GenerateFakeTestReport();

        adapter.HTTPUpload(testReport);

        mockHTTPService.Verify(m => m.PostAsync(It.IsAny<string>(), It.IsAny<CreateTestReportDTO>()), Times.Once);
    }

    [Fact]
    public void HTTPUpload_ShouldNotCallHTTPPostWhenWebConfigOptionIsSetToNotUseHTTP()
    {
        mockWebConfig.SetupGet(m => m.SendOverHTTP).Returns(false);
        var testReport = TestReportGenerator.GenerateFakeTestReport();

        adapter.HTTPUpload(testReport);

        mockHTTPService.Verify(m => m.PostAsync(It.IsAny<string>(), It.IsAny<CreateTestReportDTO>()), Times.Never);
    }

    [Fact]
    public void HTTPUpload_WhenSendOverHTTPTrue_ReturnsTask()
    {
        // Arrange
        mockWebConfig.SetupGet(m => m.SendOverHTTP).Returns(true);
        var testReport = TestReportGenerator.GenerateFakeTestReport();
        var expectedDto = new CreateTestReportDTO();
        var expectedTask = Task.FromResult(expectedDto);
        mockHTTPService.Setup(x => x.PostAsync("api/TestReport", It.IsAny<CreateTestReportDTO>()))
            .Returns(expectedTask);

        // Act
        var result = adapter.HTTPUpload(testReport);

        // Assert
        Assert.Same(expectedTask, result);
    }

    [Fact]
    public void HTTPUpload_WhenSendOverHTTPFalse_ReturnsCompletedTask()
    {
        // Arrange
        mockWebConfig.SetupGet(m => m.SendOverHTTP).Returns(false);
        var testReport = TestReportGenerator.GenerateFakeTestReport();

        // Act
        var result = adapter.HTTPUpload(testReport);

        // Assert
        Assert.Same(Task.CompletedTask, result);
    }

    [Fact]
    public async void HTTPGetTestReportsBySerialNumber_ShouldCallHTTPGet()
    {
        var testReport = TestReportGenerator.GenerateFakeTestReport();
        var dto = new List<TestReportDTO>() { DTOConverter.ToTestReportDTO(testReport) };
        var parameters = new Dictionary<string, string>
        {
            { "serialNumber", testReport.SerialNumber }
        };
        mockHTTPService
           .Setup(x => x.GetAsync<TestReportDTO>("api/TestReport", parameters))
           .ReturnsAsync(dto);

        var response = await adapter.HTTPGetTestReportsBySerialNumber(testReport.SerialNumber);

        Assert.Equal(response.First(), dto.First());
    }
}