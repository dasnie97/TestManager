using Moq;
using AutoFixture;
using TestManager.Web;
using TestEngineering.Web;
using TestManager.Configuration;
using TestEngineering.Models;
using TestEngineering.DTO;


namespace TestManagerTest;

public class WebAdapterTests
{
    private readonly WebAdapter adapter;
    private readonly Mock<IFTPService> mockFtpService;
    private readonly Mock<IWebConfig> mockWebConfig;
    private readonly Mock<IHTTPService> mockHTTPService;
    private readonly Fixture _fixture;

    public WebAdapterTests()
    {
        mockFtpService = new Mock<IFTPService>();
        mockWebConfig = new Mock<IWebConfig>();
        mockHTTPService = new Mock<IHTTPService>();
        _fixture = new Fixture();
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

    //[Fact]
    public void HTTPUpload_ShouldCallHTTPPostWhenWebConfigOptionIsSetToUseHTTP()
    {
        mockWebConfig.SetupGet(m=>m.SendOverHTTP).Returns(true);
        var testReport = _fixture.Build<TestReport>().Create();

        adapter.HTTPUpload(testReport);

        mockHTTPService.Verify(m => m.PostAsync(It.IsAny<string>(), It.IsAny<CreateTestReportDTO>()), Times.Once);
    }
}