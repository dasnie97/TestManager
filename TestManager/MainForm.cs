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

        // Determines if breakdown state is present (station malfunction)
        private bool breakdownPresent = false;
        // Handles malfunction start time
        private DateTime breakdownStarted = new DateTime();
        // Handles reference to login form. Necessary to handle logout function
        private Form loginForm;
        // IO directory paths
        private string InputDir = string.Empty;
        private string OutputDir = string.Empty;
        private string CopyDir = string.Empty;

        private int numberOfFilesProcessed = 0;
        private int numberOfFilesFailed = 0;

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
            operatorLoginLabel.Text = operatorLogin;
            loginForm = loginForm;
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
                breakdownButton.Text = "Awaria";
                breakdownPresent = false;
                ShowReportForm();
            }
            else
            {
                breakdownStarted = DateTime.Now;
                topFailuresButton.Text = "Koniec interwencji";
                breakdownPresent = true;
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
        private bool LoadConfig()
        {
            var success = true;

            foreach(var line in File.ReadLines("config.txt"))
            {
                if (line.StartsWith("//") || line == String.Empty)
                    continue;

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
        /// Show report form and reset main form if malfunction was reported succesfully
        /// </summary>
        private void ShowReportForm()
        {

            breakdownTimeStartedLabel.Text = String.Empty;
            label2.Visible = false;
        }

        private void logOutButton_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        #endregion

        #region Form events & timer

        /// <summary>
        /// Set up form on first loading
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
        /// Used for displaying time passed from start of malfunction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            if (breakdownPresent)
                breakdownTimeStartedLabel.Text = (DateTime.Now - breakdownStarted).ToString().Substring(0, 8);

            var logFiles = GetLogFiles();

            foreach (var logFile in logFiles)
            {
                LogFile LF = new(logFile);

                LF.SendTo_MySQL_DB();

                File.Copy(logFile, Path.Combine(CopyDir, Path.GetFileName(logFile)));
                File.Move(logFile, Path.Combine(OutputDir, Path.GetFileName(logFile)));

                numberOfFilesProcessed++;
                if (LF.BoardStatus != "Passed")
                    numberOfFilesFailed++;

                updateUI();
            }

        }

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
        /// When user close this form, display login form again for next user to log in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loginForm.Show();
        }

        #endregion

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
    }
}
