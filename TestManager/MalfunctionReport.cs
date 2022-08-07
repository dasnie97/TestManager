using System.Configuration;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace TestManager
{
    /// <summary>
    /// Provides functionality to report test station malfunction root causes and feedback from maintenance staff. Data can be then analyzed for improvement purposes.
    /// </summary>
    public partial class MalfunctionReport : Form
    {
        #region Private fields

        private string operatorLogin;
        private string stationName;
        private DateTime BreakdownStarted;
        private TimeSpan optionalBreakdownTimeInterval;

        #endregion

        #region Constructor

        public MalfunctionReport(string OperatorLogin, string StationName)
        {
            InitializeComponent();
            this.operatorLogin = OperatorLogin;
            this.stationName = StationName;
            this.BreakdownStarted = DateTime.Now;
            timer1.Start();
            SendQueryToDB($"UPDATE teststations SET ProblemOperator = 1 WHERE TesterName = '{stationName}';");
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

            string sql;
            // Decide if malfunction time should be caluclated basing on timer or optional textbox input
            if (optionalBreakdownTimeInterval == new TimeSpan())
            {
                sql = $@"INSERT INTO crashlog (ID,ProblemDescription,ActionTaken,Technician,StationID,Operator,TimeStarted,TimeFinished,BreakdownTotalTime) values(
                        '{Guid.NewGuid()}',
                        '{descriptionComboBox.Text} {descriptionTextbox.Text}',
                        '{actionTakenComboBox.Text} {actionTextbox.Text}',
                        '{technicianComboBox.Text}',
                        '{stationName}',
                        '{operatorLogin}',
                        '{BreakdownStarted.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                        '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                        '{DateTime.Now - BreakdownStarted}');";
            }
            else
            {
                sql = $@"INSERT INTO crashlog (ID,ProblemDescription,ActionTaken,Technician,StationID,Operator,TimeStarted,TimeFinished,BreakdownTotalTime) values(
                        '{Guid.NewGuid()}',
                        '{descriptionComboBox.Text} {descriptionTextbox.Text}',
                        '{actionTakenComboBox.Text} {actionTextbox.Text}',
                        '{technicianComboBox.Text}',
                        '{stationName}',
                        '{operatorLogin}',
                        '{(DateTime.Now - optionalBreakdownTimeInterval).ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                        '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                        '{optionalBreakdownTimeInterval}');";
            }
            SendQueryToDB(sql);
            Close();
        }

        /// <summary>
        /// Used to focus combobox
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
            SendQueryToDB($"UPDATE teststations SET ProblemOperator = 0 WHERE TesterName = '{stationName}';");
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Connects to MySQL DB, builds insertion command, inserts data into DB
        /// </summary>
        /// <param name="sql">SQL query string</param>
        private void SendQueryToDB(string sql)
        {
            try
            {
                // Create SQL handle
                MySqlConnection connection;

                var connStr = ConfigurationManager.ConnectionStrings["KitronDataBase_MySQL"].ConnectionString;

                // Open connection using connection string taken from config file of calling aplication
                connection = new MySqlConnection(connStr);

                connection.Open();

                // Execute query
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();

                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion
    }
}
