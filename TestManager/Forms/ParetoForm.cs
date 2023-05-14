using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using TestManager.Interfaces;

namespace TestManager;

public partial class ParetoForm : Form
{
    private DataPointCollection chartPoints;

    public ParetoForm(IEnumerable<ITrackedTestReport> testData)
    {
        InitializeComponent();
        const string dataSerie = "ParetoSerie";
        chartPoints = paretoChart.Series[dataSerie].Points;
        
        Dictionary<string, int> paretoData = GetParetoData(testData);
        BuildChart(paretoData);
    }

    private Dictionary<string, int> GetParetoData(IEnumerable<ITrackedTestReport> testData)
    {
        var failedTests = testData.Where(testReport => !string.IsNullOrEmpty(testReport.Failure)).ToList();
        var uniqueFailedTests = failedTests.Select(test => GetFailedStepName(test.Failure)).Distinct().ToList();
        Dictionary<string, int> paretoData = new Dictionary<string, int>();

        foreach (var uniqueFailedTest in uniqueFailedTests)
        {
            int count = failedTests.Where(test => GetFailedStepName(test.Failure) == uniqueFailedTest).Count();
            paretoData.Add(uniqueFailedTest, count);
        }

        var sortedDictionary = paretoData.OrderByDescending(x => x.Value).ToDictionary(x=>x.Key, x => x.Value);
        return sortedDictionary;
    }

    private string GetFailedStepName(string failureString)
    {
        return failureString.Split("\n")[0];
    }

    private void BuildChart(Dictionary<string, int> paretoData)
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
                AddDataPoint(failedStep.Key, failedStep.Value);
            }
            else
            {
                numberOfOtherStepsFailed += failedStep.Value;
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
