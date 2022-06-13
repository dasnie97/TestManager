using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager
{
    /// <summary>
    /// Has 2 properties. TestStepName is a name of particular test step. Quantity is number of occurences in analyzed data of this particular test step name.
    /// </summary>
    public class ParetoData
    {
        public string TestStepName { get; set; }
        public int Quantity { get; set; }

        public ParetoData(string testName, int qty)
        {
            this.TestStepName = testName;
            this.Quantity = qty;
        }
    }
}
