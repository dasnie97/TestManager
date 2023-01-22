using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManager.Transporters;

public class TransporterCreator
{
    public FileProcessor FileProcessor { get; private set; }
    private readonly Statistics _statistics;
    private readonly Config _config;
    public TransporterCreator(Statistics statistics, Config config)
    {
        _statistics = statistics;
        _config = config;
        FileProcessor = new FileProcessor(_config);
    }
    public IFileTestReportsTransporter FactoryMethod()
    {
        IFileTestReportsTransporter concreteTransporter;

        if (FileProcessor.IsDataTransferEnabled)
        {
            if (FileProcessor.TransferOption == -1 || FileProcessor.TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_statistics, FileProcessor, _config);
            }
            else if (FileProcessor.TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_statistics, FileProcessor, _config);
            }
            else if (FileProcessor.TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover(FileProcessor, _config);
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

        FileProcessor.TransferOption = -1;
        return concreteTransporter;
    }
}
