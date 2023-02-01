using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.FileHelpers;

namespace TestManager.Transporters;

public class TransporterFactory
{
    private readonly IFileProcessor _fileProcessor;
    public TransporterFactory(IFileProcessor fileProcessor)
    {
        _fileProcessor = fileProcessor;
    }
    public ITransporter GetTransporter()
    {
        ITransporter concreteTransporter;

        if (_fileProcessor.IsDataTransferEnabled)
        {
            if (_fileProcessor.TransferOption == -1 || _fileProcessor.TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_fileProcessor);
            }
            else if (_fileProcessor.TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_fileProcessor);
            }
            else if (_fileProcessor.TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover(_fileProcessor);
            }
            else
            {
                throw new InvalidOperationException("Invalid transfer option!");
            }
        }
        else
        {
            concreteTransporter = new NoFilesTransporter();
        }

        _fileProcessor.TransferOption = -1;
        return concreteTransporter;
    }
}
