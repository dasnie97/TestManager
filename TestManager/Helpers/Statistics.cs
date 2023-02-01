using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers
{
    public class Statistics : IStatistics
    {
        public int NumberOfFilesPassed { get; set; } = 0;
        public int NumberOfFilesFailed { get; set; } = 0;
        public int NumberOfFilesProcessed { get; set; } = 0;
        public List<TrackedTestReport> ProcessedData { get; set; } = new List<TrackedTestReport>();


        private Statistics() { }

        public static Statistics GetInstance()
        {
            return new Lazy<Statistics>(() => new Statistics()).Value;
        }

        public void Reset()
        {
            NumberOfFilesPassed = 0;
            NumberOfFilesFailed = 0;
            NumberOfFilesProcessed = 0;
        }
    }
}
