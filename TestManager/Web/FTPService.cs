using FluentFTP;
using Microsoft.Extensions.Configuration;

namespace FTPPlugin;

public class FTPService : IFTPService, IDisposable
{
    private readonly IConfiguration _config;
    private FtpClient _client;

    public FTPService(IConfiguration configuration)
    {
        _config = configuration;
        InitializeConnection();
    }

    public void Dispose()
    {
        CloseConnection();
    }

    //TODO: Run this asynchronously
    public void Upload(string filePath)
    {
        var status = _client.UploadFile(filePath, $"/{Path.GetFileName(filePath)}", verifyOptions: FtpVerify.Throw);
        if (status != FtpStatus.Success)
        {
            throw new Exception($"There was an error processing file {filePath}");
        }
    }

    private void InitializeConnection()
    {
        var section = _config.GetRequiredSection("FTPCredentials");
        var ftpServer = section.GetValue<string>("ftpServer");
        var ftpUsername = section.GetValue<string>("ftpUser");
        var ftpPassword = section.GetValue<string>("ftpPass");

        _client = new FtpClient(ftpServer, ftpUsername, ftpPassword);
        _client.AutoConnect();
    }

    private void CloseConnection()
    {
        _client.Disconnect();
    }
}