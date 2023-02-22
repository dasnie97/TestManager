using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class TransporterFactory : ITransporterFactory
{
    public bool IsDataTransferEnabled { get; set; } = true;
    public int TransferOption { get; set; }

    private readonly IFileProcessor _fileProcessor;
    private readonly IStatistics _statistics;

    public TransporterFactory(IFileProcessor fileProcessor, IStatistics statistics)
    {
        _statistics = statistics;
        _fileProcessor = fileProcessor;
    }
    public ITransporter GetTransporter()
    {
        ITransporter concreteTransporter;

        if (IsDataTransferEnabled)
        {
            if (TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_fileProcessor, _statistics);
            }
            else if (TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover(_fileProcessor);
            }
            else if (TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_fileProcessor, _statistics);
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
