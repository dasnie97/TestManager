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

        public Statistics UpdateStatistics(Statistics _statistics)
        {
            TestedQtyLabel.Text = _statistics.numberOfFilesProcessed.ToString();
            FailedQtyLabel.Text = _statistics.numberOfFilesFailed.ToString();

            if (_statistics.numberOfFilesProcessed != 0)
            {
                var yield = (_statistics.numberOfFilesProcessed - _statistics.numberOfFilesFailed) * 100.0 / _statistics.numberOfFilesProcessed;
                YieldLabel.Text = $"{(yield).ToString("F")}";
                if (yield >= 96)
                    YieldLabel.BackColor = Color.Green;
                else
                    YieldLabel.BackColor = Color.Red;
            }
            return _statistics;
        }
    }
}
