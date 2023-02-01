﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.FileHelpers;
using TestManager.Helpers;

namespace TestManager.Transporters;

public class TransporterFactory
{
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

        if (_fileProcessor.IsDataTransferEnabled)
        {
            if (_fileProcessor.TransferOption == 0)
            {
                concreteTransporter = new PassedFilesTransporter(_fileProcessor, _statistics);
            }
            else if (_fileProcessor.TransferOption == 1)
            {
                concreteTransporter = new AllFilesRemover(_fileProcessor);
            }
            else if (_fileProcessor.TransferOption == 2)
            {
                concreteTransporter = new AllFilesTransporter(_fileProcessor, _statistics);
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

        _fileProcessor.TransferOption = 2;
        return concreteTransporter;
    }
}
