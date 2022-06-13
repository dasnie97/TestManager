using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;
using KitronGenericTestReports;
using System.Diagnostics;

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

        // Determines if breakdown state is present (station malfunction), and data logging setting (process data or not)
        private bool breakdownPresent = false;
        private bool dataLogging = true;

        // Handles malfunction start time
        private DateTime breakdownStarted = new DateTime();

        // Handles reference to login form. Necessary to handle logout function
        private Form loginForm;

        // IO directory paths
        private string InputDir = string.Empty;
        private string OutputDir = string.Empty;
        private string CopyDir = string.Empty;

        // Holds information about processing results
        private int numberOfFilesProcessed = 0;
        private int numberOfFilesFailed = 0;

        // Holds processed log files data
        private List<LogFile> ProcessedData = new List<LogFile>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Retrieves text entered in loginForm and loginForm itself as an object.
        /// </summary>
        /// <param name="operatorLogin">Text entered in loginForm.</param>
        /// <param name="loginForm">loginForm object.</param>
        public MainForm(string operatorLogin, Form LoginForm)
        {
            InitializeComponent();

            // Assign data retrieved from loginForm to private fields
            operatorLoginLabel.Text = operatorLogin;
            loginForm = LoginForm;
        }

        #endregion

        #region Buttons

        private void logOutButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Sets dataLogging flag. ON = data is processed in main loop, OFF = data is not processed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataLoggingSwitchButton_Click(object sender, EventArgs e)
        {
            if (dataLogging == true)
            {
                dataLoggingSwitchButton.Text = "OFF";
                dataLoggingSwitchButton.BackColor = Color.Red;
                dataLogging = false;
            }
            else
            {
                dataLoggingSwitchButton.Text = "ON";
                dataLoggingSwitchButton.BackColor = Color.Green;
                dataLogging = true;
            }
        }

        /// <summary>
        /// Displays new form with chart showing top failures within processed data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void topFailuresButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create new form and pass TestData as an input parameter
                Pareto paretoForm = new Pareto(ProcessedData);

                // Show new form
                paretoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Displays new form with table of data about all processed log files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create new form and pass TestData as an input parameter
                Details detailsForm = new Details(ProcessedData);

                // Show new form
                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// Click this button to set/unset breakdownPresent flag. Click it first time to alarm technician about malfunction and stop work. Click it again to finish malfunction and show report form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakdownButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region MenuStrip

        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = InputDir,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = OutputDir,
                FileName = "explorer.exe"
            };

            Process.Start(startInfo);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = CopyDir,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
            catch { }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Get station name from txt file located in same folder as exe 
        /// </summary>
        private bool LoadConfig()
        {
            var success = true;

            foreach (var line in File.ReadLines("config.txt"))
            {
                if (line.StartsWith("TestStation"))
                {
                    var value = line.Split("\t")[1].Trim();
                    stationNameLabel.Text = value;
                }

                if (line.StartsWith("InputDir"))
                {
                    var value = line.Split("\t")[1].Trim();
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show($"Path {value} does not exist!");
                        success = false;
                    }
                    InputDir = value;
                }

                if (line.StartsWith("OutputDir"))
                {
                    var value = line.Split("\t")[1].Trim();
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show($"Path {value} does not exist!");
                        success = false;
                    }
                    OutputDir = value;
                }

                if (line.StartsWith("CopyDir"))
                {
                    var value = line.Split("\t")[1].Trim();
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show($"Path {value} does not exist!");
                        success = false;
                    }
                    CopyDir = value;
                }
            }

            if (InputDir == String.Empty)
            {
                MessageBox.Show($"Input directory does not exist!");
                success = false;
            }

            if (OutputDir == String.Empty)
            {
                MessageBox.Show($"Output directory does not exist!");
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Filters files in input dir.
        /// </summary>
        /// <returns>List of filtered files paths</returns>
        private List<string> GetLogFiles()
        {
            // Create new regular expression: dddddddd_dddddd_  where d - digit
            Regex r = new Regex(@"\d{8}\w{1}\d{6}\w{1}");


            //////////////////////////////////////////////////////////////////////////////////
            //File name example:    01032022_163836_20172797560108.txt
            //                      01052022_213920_22023520891916E.txt
            //                      01072022_123751_HID=582240701D7P2021481001056&REV=K.txt
            //////////////////////////////////////////////////////////////////////////////////


            // Assign files matching regular expression, having txt extension and with size > 0 to new list
            List<string> files = Directory.GetFiles(InputDir, "*.txt")
                     .Where(path => r.IsMatch(path) && new FileInfo(path).Length != 0)
                     .ToList();

            return files;
        }

        /// <summary>
        /// Sets statistics on processed data
        /// </summary>
        private void updateUI()
        {
            TestedQtyLabel.Text = numberOfFilesProcessed.ToString();
            FailedQtyLabel.Text = numberOfFilesFailed.ToString();

            try
            {
                var yield = (numberOfFilesProcessed - numberOfFilesFailed) * 100.0 / numberOfFilesProcessed;
                YieldLabel.Text = $"{(yield).ToString("F")}%";
                if (yield >= 96)
                    YieldLabel.BackColor = Color.Green;
                else
                    YieldLabel.BackColor = Color.Red;
            }
            catch
            {

            }
        }


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
        /// Show report form and reset main form if malfunction was reported succesfully
        /// </summary>
        private void ShowReportForm()
        {

        }

        #endregion

        #region Form events & timer

        /// <summary>
        /// Load config data, set UI controls, start timer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LoadConfig())
            {
                Close();
                return;
            }
            inputToolStripMenuItem.Text = $"Input: {InputDir}";
            outputToolStripMenuItem.Text = $"Output: {OutputDir}";
            copyToolStripMenuItem.Text = $"Copy: {CopyDir}";

            timer1000ms.Start();
        }

        /// <summary>
        /// Main loop. Displays time passed from malfunction start, process input data, update UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            // Displays time passed from malfunction start if breakdown is present
            if (breakdownPresent)
                breakdownTimeStartedLabel.Text = (DateTime.Now - breakdownStarted).ToString().Substring(0, 8);

            var logFiles = GetLogFiles();

            foreach (var logFile in logFiles)
            {
                if (dataLogging == false)
                    continue;

                LogFile LF = new(logFile);

                ProcessedData.Add(LF);

                LF.SendTo_MySQL_DB();

                if (CopyDir != String.Empty)
                    File.Copy(logFile, Path.Combine(CopyDir, Path.GetFileName(logFile)));

                File.Move(logFile, Path.Combine(OutputDir, Path.GetFileName(logFile)), true);

                numberOfFilesProcessed++;
                if (LF.BoardStatus != "Passed")
                    numberOfFilesFailed++;

                updateUI();
            }
        }

        /// <summary>
        /// When user close this form, display login form again for next user to log in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Show();
        }

        #endregion
    }
}
