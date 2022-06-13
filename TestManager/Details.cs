using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitronGenericTestReports;

namespace TestManager
{
    public partial class Details : Form
    {
        public Details(List<LogFile> InputData)
        {
            InitializeComponent();
            UpdateUI(InputData);
        }

        // Create new row in table of results
        private void UpdateUI(List<LogFile> inputData)
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
