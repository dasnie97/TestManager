using ProductTest.Common;
using System.Data;
using TestManager.Helpers;

namespace TestManager;

public partial class Pareto : Form
{
    public Pareto(IEnumerable<TestReportBase> TestData)
    {
        InitializeComponent();

        var sortedFailedTestData = getFailedTests(TestData);
        UpdateUI(sortedFailedTestData);
    }

    private void UpdateUI(List<ParetoData> sortedFailedTestData)
    {
        chart1.Series["Series1"].Points.Clear();

        int maxNumberOfFailsShowed = 4;
        if (sortedFailedTestData.Count <= maxNumberOfFailsShowed)
            maxNumberOfFailsShowed = sortedFailedTestData.Count - 1;
        int currentNumberOfFailsShowed = 0;
        int numberOfOtherStepsFailed = 0;

        foreach (var failedStep in sortedFailedTestData)
        {
            if (currentNumberOfFailsShowed <= maxNumberOfFailsShowed)
            {
                System.Windows.Forms.DataVisualization.Charting.DataPoint dP = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                dP.AxisLabel = failedStep.TestStepName;
                dP.SetValueY(failedStep.Quantity);
                dP.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
                dP.IsValueShownAsLabel = true;

                chart1.Series["Series1"].Points.Add(dP);
            }
            else
            {
                numberOfOtherStepsFailed += failedStep.Quantity;
            }
            currentNumberOfFailsShowed++;
        }

        if (numberOfOtherStepsFailed > 0)
        {
            System.Windows.Forms.DataVisualization.Charting.DataPoint other = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

            other.AxisLabel = "Other";
            other.SetValueY(numberOfOtherStepsFailed);
            other.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
            other.IsValueShownAsLabel = true;

            chart1.Series["Series1"].Points.Add(other);
        }
    }

    private List<ParetoData> getFailedTests(IEnumerable<TestReportBase> testData)
    {
        var failedTests = testData.Where(testReport => testReport.Status == "Failed" && testReport.Failure != null).ToList();
        var uniqueFailedTests = new List<string>();

        foreach (var test in failedTests)
        {
            if (uniqueFailedTests.Contains(test.Failure.Split("\n")[0]))
                continue;

            uniqueFailedTests.Add(test.Failure.Split("\n")[0]);
        }

        var pareto = new List<ParetoData>();
        foreach (var uniqueFailedTest in uniqueFailedTests)
        {
            var numberOfOccurences = 0;
            foreach (var test in failedTests)
            {
                if (uniqueFailedTest == test.Failure.Split("\n")[0])
                {
                    numberOfOccurences++;
                }
            }
            pareto.Add(new ParetoData(uniqueFailedTest, numberOfOccurences));
        }
        return pareto.OrderByDescending(x => x.Quantity).ToList();
    }
}
