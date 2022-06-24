using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace TestManager
{
    public partial class MalfunctionReport : Form
    {
        private string technicianLogin = string.Empty;
        private string operatorLogin = string.Empty;
        private DateTime breakdownFinished = new DateTime();
        private DateTime breakdownStarted = new DateTime();
        private string stationName = string.Empty;

        public MalfunctionReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Click this button to validate and send data to DataBase.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmButton_Click(object sender, EventArgs e)
        {
            // Check if textboxes are not empty
            if (this.descriptionTextbox.Text.Length == 0 || this.actionTextbox.Text.Length == 0)
            {
                MessageBox.Show("Wprowadź opis awarii oraz podjęte działania!");
                return;
            }

            // Check if text entered as technician name has correct format
            technicianLogin = technicianComboBox.Text.Trim().ToLower();

            InsertIntoDB();
        }

        /// <summary>
        /// Connects to DB, builds insertion command, inserts data into DB
        /// </summary>
        private void InsertIntoDB()
        {
            // Connect to localhost using windows credentials
            string connectionString = ConfigurationManager.ConnectionStrings["KitronDataBase_MySQL"].ConnectionString;
            SqlConnection connection;
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "";

            // Build insertion command
            sql = $@"INSERT INTO crashlog (ID,ProblemDescription,ActionTaken,Technician,StationID,Operator,TimeStarted,TimeFinished,BreakdownTotalTime) values(
                    '{Guid.NewGuid()}',
                    '{this.descriptionTextbox.Text.ToString()}',
                    '{this.actionTextbox.Text.ToString()}',
                    '{this.technicianLogin}',
                    '{this.stationName}',
                    '{this.operatorLogin}',
                    '{this.breakdownStarted.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                    '{this.breakdownFinished.ToString("yyyy-MM-dd HH:mm:ss.fff")}',
                    '{(this.breakdownFinished - this.breakdownStarted).ToString()}');";

            connection = new SqlConnection(connectionString);
            connection.Open();

            command = new SqlCommand(sql, connection);

            adapter.InsertCommand = new SqlCommand(sql, connection);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
            connection.Close();
        }

        private void MalfunctionReport_Shown(object sender, EventArgs e)
        {
            descriptionComboBox.Focus();
        }
    }
}
