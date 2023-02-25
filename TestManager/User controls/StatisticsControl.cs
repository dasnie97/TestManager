using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManager.Features.ProductionSupervision;

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
            if (Statistics.NumberOfFilesProcessed != 0)
            {
                TestedQtyLabel.Text = Statistics.NumberOfFilesProcessed.ToString();
                FailedQtyLabel.Text = Statistics.NumberOfFilesFailed.ToString();

                YieldLabel.Text = $"{(Statistics.Yield).ToString("F")}";
                if (Statistics.Yield >= 96)
                    YieldLabel.BackColor = Color.Green;
                else
                    YieldLabel.BackColor = Color.Red;
            }
        }
    }
}
