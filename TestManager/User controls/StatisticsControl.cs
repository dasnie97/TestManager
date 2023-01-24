using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Helpers;

namespace TestManager
{
    public partial class StatisticsControl : UserControl
    {
        public StatisticsControl()
        {
            InitializeComponent();
        }

        public void UpdateStatistics()
        {
            TestedQtyLabel.Text = Statistics.Instance.NumberOfFilesProcessed.ToString();
            FailedQtyLabel.Text = Statistics.Instance.NumberOfFilesFailed.ToString();

            if (Statistics.Instance.NumberOfFilesProcessed != 0)
            {
                var yield = (Statistics.Instance.NumberOfFilesProcessed - Statistics.Instance.NumberOfFilesFailed) * 100.0 / Statistics.Instance.NumberOfFilesProcessed;
                YieldLabel.Text = $"{(yield).ToString("F")}";
                if (yield >= 96)
                    YieldLabel.BackColor = Color.Green;
                else
                    YieldLabel.BackColor = Color.Red;
            }
        }
    }
}
