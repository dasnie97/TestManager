using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers
{
    public class Statistics
    {
        public int numberOfFilesPassed { get; set; } = 0;
        public int numberOfFilesFailed { get; set; } = 0;
        public int numberOfFilesProcessed { get; set; } = 0;
    }
}
