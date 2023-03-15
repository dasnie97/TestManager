using ProductTest.Common;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using TestManager.Features.Analysis;

namespace TestManager;

public partial class ParetoForm : Form
{
    private DataPointCollection chartPoints;

    public ParetoForm(IEnumerable<TestReportBase> testData)
    {
        InitializeComponent();
        const string dataSerie = "ParetoSerie";
        chartPoints = paretoChart.Series[dataSerie].Points;

        List<ParetoData> data = GetParetoData(testData);
        BuildChart(data);
    }

    private List<ParetoData> GetParetoData(IEnumerable<TestReportBase> testData)
    {
        var failedTests = testData.Where(testReport => !string.IsNullOrEmpty(testReport.Failure)).ToList();
        var uniqueFailedTests = failedTests.Select(test => GetFailedStepName(test.Failure)).Distinct().ToList();
        var paretoData = new List<ParetoData>();

        foreach (var uniqueFailedTest in uniqueFailedTests)
        {
            int count = failedTests.Where(test => GetFailedStepName(test.Failure) == uniqueFailedTest).Count();
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
        const int MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN = 5;
        var numberOfFailuresToShow = MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN;

        if (paretoData.Count < MAX_NUMBER_OF_DISTINCT_FAILURES_SHOWN)
            numberOfFailuresToShow = paretoData.Count;
        int numberOfOtherStepsFailed = 0;

        foreach (var failedStep in paretoData)
        {
            if (chartPoints.Count < numberOfFailuresToShow)
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
        dP.Font = new Font(FontFamily.GenericSansSerif, 15, FontStyle.Bold);
        dP.IsValueShownAsLabel = true;

        chartPoints.Add(dP);
    }
}
