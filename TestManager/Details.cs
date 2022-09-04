using GenericTestReport;

namespace TestManager
{
    /// <summary>
    /// Show summary of processed data in form of table.
    /// </summary>
    public partial class Details : Form
    {
        public Details(List<LogFile> InputData)
        {
            InitializeComponent();
            ShowDetails(InputData);
        }

        private void ShowDetails(List<LogFile> inputData)
        {
            foreach (var logFile in inputData)
            {
                TableOfResults.Rows.Add(logFile.BoardStatus, logFile.Path, logFile.BoardSerialNumber, logFile.FailedStep, logFile.Station, logFile.DateAndTimeOfTest);

                // Get index of last row
                var lastRowIndex = this.TableOfResults.Rows.GetLastRow(DataGridViewElementStates.Visible);

                // Set last row background color depending on LogFile status property and increment yield variables
                if (logFile.BoardStatus == "Passed")
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
}
