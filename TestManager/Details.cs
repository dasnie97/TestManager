using ProductTest.Common;

namespace TestManager;
public partial class Details : Form
{
    public Details(IEnumerable<TestReportBase> testReports)
    {
        InitializeComponent();
        ShowDetails(testReports);
    }

    private void ShowDetails(IEnumerable<TestReportBase> testReports)
    {
        foreach (var testReport in testReports)
        {
            TableOfResults.Rows.Add(testReport.Status,testReport.SerialNumber, testReport.Failure, testReport.Workstation, testReport.TestDateTimeStarted);

            // Get index of last row
            var lastRowIndex = this.TableOfResults.Rows.GetLastRow(DataGridViewElementStates.Visible);

            // Set last row background color depending on LogFile status property and increment yield variables
            if (testReport.Status == "Passed")
            {
                this.TableOfResults.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(90, 235, 33);
            }
            else
            {
                this.TableOfResults.Rows[lastRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(232, 65, 65);
            }
        }
    }
}
