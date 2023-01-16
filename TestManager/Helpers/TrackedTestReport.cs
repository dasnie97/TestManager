using ProductTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public class TrackedTestReport : TestReportBase
{
    public bool IsFirstPass { get; set; }
    public bool IsFalseCall { get; set; }
}
