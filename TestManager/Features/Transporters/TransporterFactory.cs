using TestManager.Features.ProductionSupervision;
using TestManager.Features.TrackedTestReports;
using TestManager.FileManagement;
using TestManager.Web;

namespace TestManager.Features.Transporters;

public class TransporterFactory : ITransporterFactory
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;
    private readonly IWebAdapter _webAdapter;
    private readonly ITestReportTracker _tracker;

    public TransporterFactory(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter, ITestReportTracker tracker)
    {
        _statistics = statistics;
        _fileProcessor = fileProcessor;
        _webAdapter = webAdapter;
        _tracker = tracker;
    }
    public ITransporter GetTransporter()
    {
        ITransporter transporter;

        if (IsDataTransferEnabled)
        {
            if (TransferOption == 0)
            {
                transporter = new PassedFilesTransporter(_fileProcessor, _statistics, _webAdapter, _tracker);
            }
            else if (TransferOption == 1)
            {
                transporter = new AllFilesRemover(_fileProcessor);
            }
            else if (TransferOption == 2)
            {
                transporter = new AllFilesTransporter(_fileProcessor, _statistics, _webAdapter, _tracker);
            }
            else
            {
                throw new InvalidOperationException("Invalid transfer option!");
            }

            Reset();
        }
        else
        {
            transporter = new NoFilesTransporter();
        }

        return transporter;
    }

    private void Reset()
    {
        IsDataTransferEnabled = true;
        TransferOption = 2;
    }
}
