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
        public  IStatistics Statistics { get; set; }
        public StatisticsControl()
        {
            InitializeComponent();
        }

        public void UpdateStatistics()
        {
            TestedQtyLabel.Text = Statistics.NumberOfFilesProcessed.ToString();
            FailedQtyLabel.Text = Statistics.NumberOfFilesFailed.ToString();

            if (Statistics.NumberOfFilesProcessed != 0)
            {
                var yield = (Statistics.NumberOfFilesProcessed - Statistics.NumberOfFilesFailed) * 100.0 / Statistics.NumberOfFilesProcessed;
                YieldLabel.Text = $"{(yield).ToString("F")}";
                if (yield >= 96)
                    YieldLabel.BackColor = Color.Green;
                else
                    YieldLabel.BackColor = Color.Red;
            }
        }
    }
}
