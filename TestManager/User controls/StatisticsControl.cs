using System.Threading.Tasks;
using TestManager.Interfaces;

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
            if (Statistics.NumberOfFilesProcessed != 0 && IsHandleCreated)
            {
                TestedQtyLabel.Invoke((Action)delegate
                {
                    TestedQtyLabel.Text = Statistics.NumberOfFilesProcessed.ToString();
                    FailedQtyLabel.Text = Statistics.NumberOfFilesFailed.ToString();

                    YieldLabel.Text = $"{(Statistics.Yield).ToString("F")}";
                    if (Statistics.Yield >= 96)
                        YieldLabel.BackColor = Color.Green;
                    else
                        YieldLabel.BackColor = Color.Red;
                });
            }
        }
    }
}
