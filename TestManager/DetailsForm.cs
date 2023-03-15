using ProductTest.Common;
using ProductTest.Models;

namespace TestManager;
public partial class DetailsForm : Form
{
    public DetailsForm(IEnumerable<TestReportBase> testReports)
    {
        InitializeComponent();
        ShowDetails(testReports);
    }

    private void ShowDetails(IEnumerable<TestReportBase> testReports)
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
