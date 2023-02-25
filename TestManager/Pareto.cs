using ProductTest.Common;
using ProductTest.Models;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using TestManager.Features.Analysis;

namespace TestManager;

public partial class Pareto : Form
{
    public Pareto(IEnumerable<TestReportBase> testData)
    {
        InitializeComponent();
        ChartPoints = chart1.Series["Series1"].Points;

        var paretoData = GetParetoData(testData);
        BuildChart(paretoData);
    }

    private DataPointCollection ChartPoints { get; }

    private int MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN = 5;

    private List<ParetoData> GetParetoData(IEnumerable<TestReportBase> testData)
    {
        var failedTests = testData.Where(testReport => 
            testReport.Status == TestStatus.Failed && !string.IsNullOrEmpty(testReport.Failure)
            ).ToList();

        var uniqueFailedTests = failedTests.Select(test => GetFailedStepName(test.Failure)).Distinct().ToList();
        var paretoData = new List<ParetoData>();

        foreach (var uniqueFailedTest in uniqueFailedTests)
        {
            var count = failedTests.Where(test => GetFailedStepName(test.Failure) == uniqueFailedTest).Count();
            paretoData.Add(new ParetoData(uniqueFailedTest, count));
        }

        return paretoData.OrderByDescending(x => x.Quantity).ToList();
    }

    private string GetFailedStepName(string failureString)
    {
        return failureString.Split("\n")[0];
    }

    private void BuildChart(List<ParetoData> paretoData)
    {
        if (paretoData.Count < MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN)
            MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN = paretoData.Count;
        int numberOfOtherStepsFailed = 0;

        foreach (var failedStep in paretoData)
        {
            if (ChartPoints.Count < MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN)
            {
                AddDataPoint(failedStep.TestStepName, failedStep.Quantity);
            }
            else
            {
                numberOfOtherStepsFailed += failedStep.Quantity;
            }
        }

        if (numberOfOtherStepsFailed > 0)
        {
            AddDataPoint("other", numberOfOtherStepsFailed);
        }
    }

    private void AddDataPoint(string name, int value)
    {
        DataPoint dP = new DataPoint();

        dP.AxisLabel = name;
        dP.SetValueY(value);
        dP.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
        dP.IsValueShownAsLabel = true;

        ChartPoints.Add(dP);
    }
}
