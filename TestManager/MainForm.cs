using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;

namespace TestManager
{
    /// <summary>
    /// Test manager is complex tool for handling test data. It performs several functions:
    /// Informs user about: currently working operator, test station name, tester performance (yield, tested quantity).
    /// Provides functionality to: send test data to factory traceability system and to test data base (this can be turned on and off using button), display top failure causes, display details about every tested board since startup.
    /// Also provides functionality for operator to raise ana alarm when malfunction occurs. Maintenance technicians are informed and once tester is repaired, technician can report what was cause of problem and action taken. This data along with operator name, station ID, starting and finishing time of malfunction, and technician name are then send to data base for further analysis.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private fields

        private bool breakdownPresent = false;
        private DateTime breakdownStarted = new DateTime();
        private string stationName = string.Empty;
        private Form loginForm;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Retrieves text entered in loginForm and loginForm itself as an object.
        /// </summary>
        /// <param name="operatorLogin">Text entered in loginForm.</param>
        /// <param name="loginForm">loginForm object.</param>
        public MainForm(string operatorLogin, Form loginForm)
        {
            InitializeComponent();

            // Assign data retrieved from loginForm to private fields
            this.operatorLoginLabel.Text = operatorLogin;
            this.loginForm = loginForm;
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Click this button to set/unset breakdownPresent flag. Click it first time to alarm technician about malfunction and stop work. Click it again to finish malfunction and show report form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakdownButton_Click(object sender, EventArgs e)
        {
            if (breakdownPresent)
            {
                this.breakdownButton.Text = "Awaria";
                this.breakdownPresent = false;
                ShowReportForm();
            }
            else
            {
                this.breakdownStarted = DateTime.Now;
                this.topFailuresButton.Text = "Koniec interwencji";
                this.breakdownPresent = true;
                RaiseAlarm();
                StopWork();
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Block work on this test station
        /// </summary>
        private void StopWork()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inform technicians about malfunction of test station
        /// </summary>
        private void RaiseAlarm()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get station name from txt file located in same folder as exe 
        /// </summary>
        private void GetStationName()
        {
            foreach(var line in File.ReadLines("config.txt"))
            {
                if (line.StartsWith("//") || line == String.Empty)
                    continue;
                this.stationName = line;
            }
        }

        /// <summary>
        /// Show report form and reset main form if malfunction was reported succesfully
        /// </summary>
        private void ShowReportForm()
        {

            this.breakdownTimeStartedLabel.Text = String.Empty;
            this.label2.Visible = false;
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Form events & timer

        /// <summary>
        /// Set up form on first loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1000ms.Start();
            GetStationName();
            this.stationNameLabel.Text = this.stationName;
        }

        /// <summary>
        /// Used for displaying time passed from start of malfunction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            if (this.breakdownPresent)
                this.breakdownTimeStartedLabel.Text = (DateTime.Now - this.breakdownStarted).ToString().Substring(0, 8);
        }

        /// <summary>
        /// When user close this form, display login form again for next user to log in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.loginForm.Show();
        }

        #endregion
    }
}
