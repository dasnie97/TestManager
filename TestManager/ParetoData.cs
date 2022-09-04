namespace TestManager
{
    public class ParetoData
    {
        /// <summary>
        /// Name of particular test step.
        /// </summary>
        public string TestStepName { get; set; }

        /// <summary>
        /// Number of occurences in analyzed data of this particular test step name.
        /// </summary>
        public int Quantity { get; set; }

        public ParetoData(string testName, int qty)
        {
            this.TestStepName = testName;
            this.Quantity = qty;
        }
    }
}
