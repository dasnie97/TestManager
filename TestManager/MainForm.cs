using System.Text.RegularExpressions;
using GenericTestReport;
using System.Diagnostics;
using System.Configuration;
using GenericTestReport.Interfaces;

namespace TestManager
{
    public partial class MainForm : Form
    {
        #region Private fields

        private bool dataLogging = true;
        private int loggingOption = -1;

        /// <summary>
        /// Reference to login form. Necessary to handle logout function
        /// </summary>
        private Form loginForm;
        private string operatorName;

        private string InputDir = string.Empty;
        private string OutputDir = string.Empty;
        private string CopyDir = string.Empty;
        private bool SendOverFTP = false;
        private bool SendOverHTTP = false;
        private bool VerifyMES = false;
        private bool Verify3510 = false;
        private string TestStationName = string.Empty;

        private int numberOfFilesProcessed = 0;
        private int numberOfFilesFailed = 0;
        private int previousNumberOfFiles = 0;

        private List<LogFile> ProcessedData = new List<LogFile>();

        private HTTPPlugin _httpService = new HTTPPlugin();
        private MESPlugin _mesService = new MESPlugin();
        private FTPPlugin _ftpService = new FTPPlugin();

        private Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        #endregion

        #region Constructor

        public MainForm(string operatorLogin, Form LoginForm)
        {
            InitializeComponent();
            operatorName = operatorLogin;
            loginForm = LoginForm;
            operatorLoginLabel.Text = operatorName;
        }

        #endregion

        #region Buttons

        private void logOutButton_Click(object sender, EventArgs e)
        {
            Close();
        }

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

        private void topFailuresButton_Click(object sender, EventArgs e)
        {
            try
            {
                Pareto paretoForm = new Pareto(ProcessedData);
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
                Details detailsForm = new Details(ProcessedData);
                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void breakdownButton_Click(object sender, EventArgs e)
        {
            TurnOffLogging();
            try
            {
                MalfunctionReport malfForm = new MalfunctionReport(operatorName, TestStationName);
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

        private void ftpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configFile.AppSettings.Settings["SendOverFTP"].Value = Convert.ToString(ftpToolStripMenuItem.Checked);
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            LoadConfig();
        }

        private void mesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configFile.AppSettings.Settings["VerifyMES"].Value = Convert.ToString(mesToolStripMenuItem.Checked);
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            LoadConfig();
        }

        private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configFile.AppSettings.Settings["Verify3510"].Value = Convert.ToString(verify3510ToolStripMenuItem.Checked);
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            LoadConfig();
        }

        private void httpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configFile.AppSettings.Settings["SendOverHTTP"].Value = Convert.ToString(httpToolStripMenuItem.Checked);
            _configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(_configFile.AppSettings.SectionInformation.Name);
            LoadConfig();
        }

        #endregion

        #region Private methods

        private bool LoadConfig()
        {
            var success = true;

            var _cfg = _configFile.AppSettings.Settings;
            
            TestStationName = _cfg["TestStationName"].Value ?? "Not found";
            stationNameLabel.Text = TestStationName;
            try
            {
                if (TestStationName == "Not found") throw new Exception("Test station name not found!");
                SendOverFTP = Convert.ToBoolean(_cfg["SendOverFTP"].Value);
                SendOverHTTP = Convert.ToBoolean(_cfg["SendOverHTTP"].Value);
                VerifyMES = Convert.ToBoolean(_cfg["VerifyMES"].Value);
                Verify3510 = Convert.ToBoolean(_cfg["Verify3510"].Value);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error loading config: {e.Message}");
                success = false;
            }

            ftpToolStripMenuItem.Checked = SendOverFTP;
            httpToolStripMenuItem.Checked = SendOverHTTP;
            mesToolStripMenuItem.Checked = VerifyMES;
            verify3510ToolStripMenuItem.Checked = Verify3510;

            var input = _cfg["InputDir"].Value;
            if (!string.IsNullOrEmpty(input))
            {
                if (!Directory.Exists(input))
                {
                    MessageBox.Show($"Path {input} does not exist!");
                    success = false;
                }
                else
                {
                    InputDir = input;
                    inputToolStripMenuItem.Text = $"Input: {InputDir}";
                }
            }

            var output = _cfg["OutputDir"].Value;
            if (!string.IsNullOrEmpty(output))
            {
                if (!Directory.Exists(output))
                {
                    MessageBox.Show($"Path {output} does not exist!");
                    success = false;
                }
                else
                {
                    OutputDir = output;
                    outputToolStripMenuItem.Text = $"Output: {OutputDir}";
                }
            }

            var copy = _cfg["CopyDir"].Value;
            if (!string.IsNullOrEmpty(copy))
            {
                if (!Directory.Exists(copy))
                {
                    MessageBox.Show($"Path {copy} does not exist!");
                    success = false;
                }
                else
                {
                    CopyDir = copy;
                    copyToolStripMenuItem.Text = $"Copy: {CopyDir}";
                }
            }
            
            return success;
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

        private async Task ProcessLogFiles(List<string> logFiles)
        {
            timer3000ms.Stop();
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
                    LF.Operator = operatorName;

                    // Filter data basing on sending option setting
                    // Transfer only passed
                    if (loggingOption == 0)
                    {
                        if (LF.Status != "Passed")
                        {
                            File.Delete(logFile);
                            continue;
                        }
                    }
                    // Delete all files and transfer nothing
                    if (loggingOption == 1)
                    {
                        File.Delete(logFile);
                        continue;
                    }
                    // Transfer all
                    if (loggingOption == 2)
                    {}

                    if (SendOverHTTP) await _httpService.HttpPost(LF);
                    if (SendOverFTP) await _ftpService.SendToFTP(logFile);
                    if (CopyDir != String.Empty) CopyFile(logFile);
                    MoveFile(logFile);

                    ProcessedData.Add(LF);
                    numberOfFilesProcessed++;
                    if (LF.Status != "Passed")
                        numberOfFilesFailed++;

                    updateUI();
                }
                if (loggingOption != -1)
                    loggingOption = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
                Close();
            }
            timer3000ms.Start();
        }

        private void CopyFile(string logFile)
        {
            var cdir = Path.Combine(CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
            Directory.CreateDirectory(cdir);
            File.Copy(logFile, Path.Combine(cdir, Path.GetFileName(logFile)));
        }

        private void MoveFile(string logFile)
        {
            var mdir = Path.Combine(OutputDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
            Directory.CreateDirectory(mdir);
            File.Move(logFile, Path.Combine(mdir, Path.GetFileName(logFile)), true);
        }

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

        private void TurnOffLogging()
        {
            dataLoggingSwitchButton.Text = "OFF";
            dataLoggingSwitchButton.BackColor = Color.Red;
            dataLogging = false;
            sendOptionCombobox.Visible = true;
            sendOptionCombobox.SelectedIndex = 0;
        }

        private void TurnOnLogging()
        {
            dataLoggingSwitchButton.Text = "ON";
            dataLoggingSwitchButton.BackColor = Color.Green;
            dataLogging = true;
            sendOptionCombobox.Visible = false;
            loggingOption = sendOptionCombobox.SelectedIndex;
        }

        /// <summary>
        /// Asynchronous method for checking if test data is transferred correctly into MES.
        /// </summary>
        /// <param name="lF">Represents currently processed test data object.</param>
        /// <returns>Returned value is not used.</returns>
        private async Task<LogFile> CheckMES(LogFile lF)
        {
            try
            {
                var alarm = true;
                await Task.Run(() =>
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (_mesService.TestDataPresentInSystem(lF))
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
                    //sqlHandle.UpdateTeststations(problemFLX: "1");
                }
                else
                {
                    //sqlHandle.UpdateTeststations(problemFLX: "0");
                }
                return lF;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.Message);
                Close();
                return lF;
            }
        }

        #endregion

        #region Form events & timer

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LoadConfig())
            {
                Close();
                return;
            }
            if (!WorkstationExists()) CreateWorkstation();
            TurnOffLogging();
            timer3000ms.Start();
        }

        private void CreateWorkstation()
        {
            var workstation = new Workstation(TestStationName);
            _httpService.HttpPost<Workstation>(workstation);
        }

        private bool WorkstationExists()
        {
            List<Workstation> workstations = _httpService.HttpGet<Workstation>().Result;
            return workstations.Any(x => x.Name == TestStationName) ? true : false;
        }

        // Main loop
        private void timer3000ms_Tick(object sender, EventArgs e)
        {
            if (dataLogging != false)
            {
                var logFiles = GetLogFiles();
                ProcessLogFiles(logFiles);
                if (logFiles.Count > 0 && VerifyMES)
                {
                    Task task = CheckMES(ProcessedData.Last());
                }
            }     
        }

        private void timer20min_Tick(object sender, EventArgs e)
        {
            var estimatedOutput = TimeSpan.FromSeconds(ProcessedData.Where(x => x.Status == "Passed").Select(x=>x.TestingTime).Average(spans=>spans.TotalSeconds));
            if (numberOfFilesProcessed > previousNumberOfFiles )
            previousNumberOfFiles = numberOfFilesProcessed;
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
