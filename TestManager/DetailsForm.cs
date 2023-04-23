using TestEngineering.Models;
using TestManager.Features.TrackedTestReports;

namespace TestManager;
public partial class DetailsForm : Form
{
    public DetailsForm(IEnumerable<ITrackedTestReport> testReports)
    {
        InitializeComponent();
        ShowDetails(testReports);
    }

    private void ShowDetails(IEnumerable<ITrackedTestReport> testReports)
    {
        foreach (var testReport in testReports)
        {
            TableOfResults.Rows.Add(testReport.Status, testReport.SerialNumber, testReport.Failure, testReport.Workstation.Name, testReport.TestDateTimeStarted);

            var lastRowIndex = TableOfResults.Rows.GetLastRow(DataGridViewElementStates.Visible);

            if (testReport.Status == TestStatus.Passed)
            {
                //Green
                TableOfResults.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(90, 235, 33);
            }
            else
            {
                //Red
                TableOfResults.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(232, 65, 65);
            }
        }
    }
}
