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

        // Determines data logging setting (process data or not)
        private bool dataLogging = true;

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

        // Holds log files sending option
        private int sendingOption = -1;

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
                TurnOffLogging();
            }
            else
            {
                TurnOnLogging();
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
        /// Click this button to alarm technician about malfunction and stop work.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakdownButton_Click(object sender, EventArgs e)
        {
            TurnOffLogging();
            try
            {
                // Create new form and pass TestData as an input parameter
                MalfunctionReport malfForm = new MalfunctionReport(operatorLoginLabel.Text, stationNameLabel.Text);

                // Show new form
                malfForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

            var configPath = "config.txt";

            if (!File.Exists(configPath))
            {
                MessageBox.Show($"Path {configPath} does not exist!");
                return false;
            }

            foreach (var line in File.ReadLines(configPath))
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
                YieldLabel.Text = $"{(yield).ToString("F")}";
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
        /// Process test log files basing on sending option setting. Create LogFile object, add it to list of log file objects, send data to data base, move and copy file, 
        /// calculate statistics and display it in UI.
        /// </summary>
        /// <param name="logFiles">List of input data paths</param>
        private void ProcessLogFiles(List<string> logFiles)
        {
            try
            {
                Thread.Sleep(2000);
                foreach (var logFile in logFiles)
                {
                    LogFile LF = new(logFile);

                    // Filter data basing on sending option setting
                    if (sendingOption == 0)
                    {
                        if (LF.BoardStatus != "Passed")
                        {
                            File.Delete(logFile);
                            continue;
                        }
                    }
                    if (sendingOption == 1)
                    {
                        File.Delete(logFile);
                        continue;
                    }
                    if (sendingOption == 2)
                    {}

                    // Add object to list for displaying Details and Pareto forms
                    ProcessedData.Add(LF);

                    var err = LF.SendTo_MySQL_DB(new string[] {"Operator", operatorLoginLabel.Text});
                    if (err.Length > 2)
                    {
                        throw new Exception(err);
                    }

                    if (CopyDir != String.Empty)
                        File.Copy(logFile, Path.Combine(CopyDir, Path.GetFileName(logFile)));

                    File.Move(logFile, Path.Combine(OutputDir, Path.GetFileName(logFile)), true);

                    numberOfFilesProcessed++;
                    if (LF.BoardStatus != "Passed")
                        numberOfFilesFailed++;

                    updateUI();
                }
                if (sendingOption != -1)
                    sendingOption = -1;
            }
            catch (Exception ex)
            {
                timer3000ms.Stop();
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        /// <summary>
        /// Sets data logging mode OFF
        /// </summary>
        private void TurnOffLogging()
        {
            dataLoggingSwitchButton.Text = "OFF";
            dataLoggingSwitchButton.BackColor = Color.Red;
            dataLogging = false;
            sendOptionCombobox.Visible = true;
            sendOptionCombobox.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets data logging ON
        /// </summary>
        private void TurnOnLogging()
        {
            dataLoggingSwitchButton.Text = "ON";
            dataLoggingSwitchButton.BackColor = Color.Green;
            dataLogging = true;
            sendOptionCombobox.Visible = false;
            sendingOption = sendOptionCombobox.SelectedIndex;
        }

        /// <summary>
        /// Block work on this test station
        /// </summary>
        private void StopWork()
        {
            throw new NotImplementedException();
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

            timer3000ms.Start();
        }

        /// <summary>
        /// Main loop. Displays time passed from malfunction start, process input data, update UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            // Process log files if data logging is turned on
            if (dataLogging != false)
            {
                var logFiles = GetLogFiles();
                ProcessLogFiles(logFiles);
            }     
        }

        /// <summary>
        /// When user close this form, display login form again for next user to log in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Commented to ommit operator login
            //loginForm.Show();
        }

        #endregion
    }
}
