using System.Text.RegularExpressions;

namespace TestManager
{
    /// <summary>
    /// Provides functionality for reporting test station malfunction root causes and feedback from maintenance staff. Data can be then analyzed for improvement purposes.
    /// </summary>
    public partial class MalfunctionReport : Form
    {
        #region Fields

        /// <summary>
        /// Date and time of malfunction start
        /// </summary>
        public DateTime BreakdownStarted;

        /// <summary>
        /// Time interval entered by user manually. Optional feature.
        /// </summary>
        public TimeSpan optionalBreakdownTimeInterval;

        /// <summary>
        /// Handles all sql traffic.
        /// </summary>
        //private HTTPPlugin<LogFile> _httpService = new();

        #endregion

        #region Constructor

        /// <summary>
        /// Create malfunction reporting form. Sets flag in DB indicating problem with tester.
        /// </summary>
        /// <param name="OperatorLogin"></param>
        /// <param name="StationName"></param>
        public MalfunctionReport(string OperatorLogin, string StationName)
        {
            InitializeComponent();
            BreakdownStarted = DateTime.Now;
            timer1.Start();
            //_httpService.UpdateTeststations(problemOperator: "1");
        }

        #endregion

        #region Controls

        /// <summary>
        /// Click this button to validate and send data to DataBase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            // Check if login combobox is not empty
            if (this.technicianComboBox.Text.Length == 0)
            {
                MessageBox.Show("Wprowadź swój login!");
                return;
            }
            // Check if comboboxes are not empty
            if (this.descriptionComboBox.Text.Length == 0 || this.actionTakenComboBox.Text.Length == 0)
            {
                MessageBox.Show("Wprowadź opis awarii oraz podjęte działania!");
                return;
            }
            // Check if optional textbox is empty, validate and get data from it
            if (malfunctionTimeOptionalTextBox.Text.Length != 0)
            {
                var minutes = malfunctionTimeOptionalTextBox.Text.Trim();

                // Check if textbox contains sequence of 1, 2 or 3 digits 
                Regex r = new Regex(@"\d{1,3}");
                Match m = r.Match(minutes);

                // Check if regex succedded and data is no longer than 3
                if (!m.Success || minutes.Length > 3)
                {
                    MessageBox.Show("Wprowadź odpowiednią ilość minut do opcjonalnego pola czasu trwania awarii!");
                    malfunctionTimeOptionalTextBox.Clear();
                    return;
                }

                // Get data into TimeSpan object
                try
                {
                    var timeSpan = float.Parse(minutes);
                    optionalBreakdownTimeInterval = TimeSpan.FromMinutes(timeSpan);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            // Decide if malfunction time should be caluclated basing on timer or optional textbox input
            //if (optionalBreakdownTimeInterval == new TimeSpan())
            //{
            //    sqlHandle.CrashStarted = BreakdownStarted.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //    sqlHandle.CrashTime = (DateTime.Now - BreakdownStarted).ToString("hh\\:mm\\:ss");
            //}
            //else
            //{
            //    sqlHandle.CrashStarted = (DateTime.Now - optionalBreakdownTimeInterval).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //    sqlHandle.CrashTime = optionalBreakdownTimeInterval.ToString("hh\\:mm\\:ss");
            //}
            //sqlHandle.ProblemDescription = descriptionComboBox.Text + " " + descriptionTextbox.Text;
            //sqlHandle.ActionTaken = actionTakenComboBox.Text + " " + actionTextbox.Text;
            //sqlHandle.Technician = technicianComboBox.Text;
            //sqlHandle.InsertCrashlog();
            Close();
        }

        /// <summary>
        /// Focus combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MalfunctionReport_Shown(object sender, EventArgs e)
        {
            technicianComboBox.Focus();
        }

        /// <summary>
        /// Used for displaying time passed from malfunction start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            breakdownTimeStartedLabel.Text = (DateTime.Now - BreakdownStarted).ToString().Substring(0, 8);
        }

        /// <summary>
        /// Update DB when form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MalfunctionReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            //sqlHandle.UpdateTeststations(problemOperator: "0");
        }

        #endregion
    }
}
