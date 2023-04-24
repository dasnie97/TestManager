using Moq;
using TestManager.Web;
using TestEngineering.Web;
using TestManager.Configuration;

namespace TestManagerTest;

public class WebAdapterTests
{
    private readonly WebAdapter adapter;
    private readonly Mock<IFTPService> mockFtpService;
    private readonly Mock<IWebConfig> mockWebConfig;
    private readonly Mock<IHTTPService> mockHTTPService;

    public WebAdapterTests()
    {
        mockFtpService = new Mock<IFTPService>();
        mockWebConfig = new Mock<IWebConfig>();
        mockHTTPService = new Mock<IHTTPService>();
        adapter = new WebAdapter(mockWebConfig.Object, mockFtpService.Object, mockHTTPService.Object);
    }

    [Fact]
    public void FTPUpload_ShouldUploadFileToFTPWhenWebConfigOptionIsSetToUseFTP()
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
    public void FTPUpload_ShouldUploadFileToFTPWhenWebConfigOptionIsSetToNotUseFTP()
    {
        // Arrange
        string localFilePath = "testfile.txt";
        mockWebConfig.SetupGet(m => m.SendOverFTP).Returns(false);

        // Act
        adapter.FTPUpload(localFilePath);

        // Assert
        mockFtpService.Verify(m => m.Upload(localFilePath), Times.Never);
    }
}