using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;
using TestManager.Interfaces;

namespace TestManager.Transporters;

public class NoFilesTransporter : IFileTestReportsTransporter
{
    public NoFilesTransporter()
    {
        
    }
    public void TransportTestReports()
    {
        // No files are transported
    }
}
