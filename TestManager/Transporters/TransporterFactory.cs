using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManager.Transporters;

public class TransporterFactory
{
    private readonly Statistics _statistics;
    public TransporterFactory(Statistics statistics)
    {
        _statistics = statistics;
    }
    public ITransporter GetTransporter()
    {
        ITransporter concreteTransporter;

        if (FileProcessor.Instance.IsDataTransferEnabled)
        {
            if (FileProcessor.Instance.TransferOption == -1 || FileProcessor.Instance.TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_statistics);
            }
            else if (FileProcessor.Instance.TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_statistics);
            }
            else if (FileProcessor.Instance.TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover();
            }
            else
            {
                throw new Exception("Invalid transfer option!");
            }
        }
        else
        {
            concreteTransporter = new NoFilesTransporter();
        }

        FileProcessor.Instance.TransferOption = -1;
        return concreteTransporter;
    }
}
