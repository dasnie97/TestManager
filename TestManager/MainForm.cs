using System.Text.RegularExpressions;
using GenericTestReport;
using System.Diagnostics;
using System.Configuration;

namespace TestManager
{
    /// <summary>
    /// TestManager main form.
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private fields

        /// <summary>
        /// Data transfer setting (process data or not)
        /// </summary>
        private bool dataLogging = true;

        /// <summary>
        /// Reference to login form. Necessary to handle logout function
        /// </summary>
        private Form loginForm;

        /// <summary>
        /// IO directory paths
        /// </summary>
        private string InputDir = string.Empty;
        private string OutputDir = string.Empty;
        private string CopyDir = string.Empty;

        /// <summary>
        /// Statistics
        /// </summary>
        private int numberOfFilesProcessed = 0;
        private int numberOfFilesFailed = 0;

        /// <summary>
        /// Processed log files data
        /// </summary>
        private List<LogFile> ProcessedData = new List<LogFile>();

        /// <summary>
        /// Data transfer option
        /// </summary>
        private int sendingOption = -1;

        /// <summary>
        /// Sql traffic handle
        /// </summary>
        private MySQLManager sqlHandle;

        /// <summary>
        /// Describes if data should be send to FTP server
        /// </summary>
        private bool SendToFTP = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Retrieves text entered in loginForm and loginForm itself as an object.
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
        /// Sets dataLogging flag. ON = data is transferred, OFF = data is not transferred
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
                Pareto paretoForm = new Pareto(this.ProcessedData);
                paretoForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Displays new form with table of results since startup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void detailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                Details detailsForm = new Details(ProcessedData);
                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Click this button to alarm technician about malfunction and stop transferring data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void breakdownButton_Click(object sender, EventArgs e)
        {
            TurnOffLogging();
            try
            {
                MalfunctionReport malfForm = new MalfunctionReport(operatorLoginLabel.Text, stationNameLabel.Text);
                malfForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region MenuStrip

        /// <summary>
        /// Open directory in file explorer by clicking on menu strip item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = InputDir,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// Open directory in file explorer by clicking on menu strip item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = OutputDir,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// Open directory in file explorer by clicking on menu strip item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// Get configuration settings from txt file located in same folder as exe 
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
                if (line.StartsWith("TestStation:\t"))
                {
                    var value = line.Split("\t")[1].Trim();
                    stationNameLabel.Text = value;
                }

                if (line.StartsWith("InputDir:\t"))
                {
                    var value = line.Split("\t")[1].Trim();
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show($"Path {value} does not exist!");
                        success = false;
                    }
                    InputDir = value;
                }

                if (line.StartsWith("OutputDir:\t"))
                {
                    var value = line.Split("\t")[1].Trim();
                    if (!Directory.Exists(value))
                    {
                        MessageBox.Show($"Path {value} does not exist!");
                        success = false;
                    }
                    OutputDir = value;
                }

                if (line.StartsWith("CopyDir:\t"))
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
            { }
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
                foreach (var logFile in logFiles)
                {
                    // Wait until file is released
                    for (int i = 0; i < 5; i++)
                    {
                        if (!IsFileLocked(new FileInfo(logFile)))
                            break;
                        Thread.Sleep(100);
                    }

                    LogFile LF = new(logFile);

                    // Filter data basing on sending option setting
                    // Transfer only passed
                    if (sendingOption == 0)
                    {
                        if (LF.BoardStatus != "Passed")
                        {
                            File.Delete(logFile);
                            continue;
                        }
                    }
                    // Delete all files and transfer nothing
                    if (sendingOption == 1)
                    {
                        File.Delete(logFile);
                        continue;
                    }
                    // Transfer all
                    if (sendingOption == 2)
                    {}

                    // Add object to list for displaying Details and Pareto forms
                    ProcessedData.Add(LF);

                    if (SendToFTP == true)
                    {
                        sqlHandle.SendToFTP(LF);
                    }
                    else
                    {
                        sqlHandle.InsertTestResult(LF);
                    }

                    if (CopyDir != String.Empty)
                    {
                        var cdir = Path.Combine(CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
                        Directory.CreateDirectory(cdir);
                        File.Copy(logFile, Path.Combine(cdir, Path.GetFileName(logFile)));
                    }

                    var mdir = Path.Combine(OutputDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
                    Directory.CreateDirectory(mdir);
                    File.Move(logFile, Path.Combine(mdir, Path.GetFileName(logFile)), true);

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
        /// Checks if file is ready to process.
        /// </summary>
        /// <param name="file">Path to file.</param>
        /// <returns>True if file is used by another process. False if file is free.</returns>
        private bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            //file is not locked
            return false;
        }

        /// <summary>
        /// Sets data processing mode OFF
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
        /// Sets data processing ON
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
        /// Asynchronous method for checking if test data is transferred correctly into factory traceability system.
        /// </summary>
        /// <param name="lF">Represents currently processed test data object.</param>
        /// <returns>Returned value is not used.</returns>
        private async Task<LogFile> CheckLogInSystem(LogFile lF)
        {
            try
            {
                var alarm = true;
                await Task.Run(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (sqlHandle.TestDataPresentInSystem(lF))
                        {
                            alarm = false;
                            break;
                        }
                        else
                        {
                            alarm = true;
                            Task.Delay(60000).Wait();
                        }
                    }
                });

                if (alarm)
                {
                    dataLoggingSwitchButton.PerformClick();
                    sqlHandle.UpdateTeststations(problemFLX: "1");
                }
                else
                {
                    sqlHandle.UpdateTeststations(problemFLX: "0");
                }
                return lF;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
                return lF;
            }

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

            var ftpOpt = ConfigurationManager.AppSettings.Get("SendToFTP");
            if (ftpOpt == "1")
            {
                SendToFTP = true;
                ToolStripItem ftpStripItem = new ToolStripMenuItem("FTP");
                menuStrip1.Items.Add(ftpStripItem);
            }
            inputToolStripMenuItem.Text = $"Input: {InputDir}";
            outputToolStripMenuItem.Text = $"Output: {OutputDir}";
            copyToolStripMenuItem.Text = $"Copy: {CopyDir}";

            timer3000ms.Start();
            this.sqlHandle = new MySQLManager(sName: this.stationNameLabel.Text, oName: this.operatorLoginLabel.Text);
        }

        /// <summary>
        /// Main loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1000ms_Tick(object sender, EventArgs e)
        {
            if (dataLogging != false)
            {
                var logFiles = GetLogFiles();
                ProcessLogFiles(logFiles);

                // Check if data flows to factory system. If no - stop work (dataLogging false) and raise alarm.
                if (logFiles.Count > 0 && SendToFTP == false)
                {
                    Task task = CheckLogInSystem(ProcessedData.Last());
                }
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
