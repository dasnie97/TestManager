using TestManager.Features.ProductionSupervision;
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

    public TransporterFactory(IFileProcessor fileProcessor, IStatistics statistics, IWebAdapter webAdapter)
    {
        _statistics = statistics;
        _fileProcessor = fileProcessor;
        _webAdapter = webAdapter;
    }
    public ITransporter GetTransporter()
    {
        ITransporter concreteTransporter;

        if (IsDataTransferEnabled)
        {
            if (TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_fileProcessor, _statistics, _webAdapter);
            }
            else if (TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover(_fileProcessor);
            }
            else if (TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_fileProcessor, _statistics, _webAdapter);
            }
            else
            {
                throw new InvalidOperationException("Invalid transfer option!");
            }

            Reset();
        }
        else
        {
            concreteTransporter = new NoFilesTransporter();
        }

        return concreteTransporter;
    }

    private void Reset()
    {
        IsDataTransferEnabled = true;
        TransferOption = 2;
    }
}
