namespace TestManager.Helpers
{
    public class ParetoData
    {
        public string TestStepName { get; set; }
        public int Quantity { get; set; }

        public ParetoData(string testName, int qty)
        {
            TestStepName = testName;
            Quantity = qty;
        }
    }
}
