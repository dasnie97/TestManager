using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public interface IWorkstation
{
    public string Name { get; set; }
    public string OperatorName { get; set; }
}
