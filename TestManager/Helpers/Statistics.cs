using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers
{
    public class Statistics
    {
        public static Statistics Instance { get { return lazy.Value; } }
        public int NumberOfFilesPassed { get; set; } = 0;
        public int NumberOfFilesFailed { get; set; } = 0;
        public int NumberOfFilesProcessed { get; set; } = 0;

        private static readonly Lazy<Statistics> lazy = new Lazy<Statistics>(() => new Statistics());

        private Statistics() { }

        public void Reset()
        {
            NumberOfFilesPassed = 0;
            NumberOfFilesFailed = 0;
            NumberOfFilesProcessed = 0;
        }
    }
}
