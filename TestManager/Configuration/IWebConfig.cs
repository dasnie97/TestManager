using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.ConfigHelpers;

public interface IWebConfig
{
    public bool SendOverFTP { get; }
    public bool SendOverHTTP { get; }
    public bool VerifyMES { get; }
    public bool Verify3510 { get; }
}
