using FluentFTP;
using Microsoft.Extensions.Configuration;

namespace FTPPlugin;

public class FTPService : IFTPService
{
    private readonly IConfiguration _config;
    public FTPService(IConfiguration configuration)
    {
        _config = configuration;
    }

    //TODO: Run this asynchronously
    public void Upload(string filePath)
    {
        var section = _config.GetRequiredSection("FTPCredentials");
        var ftpServer = section.GetValue<string>("ftpServer");
        var ftpUsername = section.GetValue<string>("ftpUser");
        var ftpPassword = section.GetValue<string>("ftpPass");

        var client = new FtpClient(ftpServer, ftpUsername, ftpPassword);
        client.AutoConnect();
        client.UploadFile(filePath, $"/{Path.GetFileName(filePath)}", verifyOptions: FtpVerify.Throw);
        client.Disconnect();
    }
}