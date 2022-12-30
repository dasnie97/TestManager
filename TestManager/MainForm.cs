using System.Text.RegularExpressions;
using GenericTestReport;
using System.Diagnostics;
using System.Configuration;
using System.Net.Http.Json;
using TestManager.Helpers;

namespace TestManager;

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

    private List<LogFile> ProcessedData = new List<LogFile>();

    private HTTPPlugin _httpService = new HTTPPlugin();
    private MESPlugin _mesService = new MESPlugin();
    private FTPPlugin _ftpService = new FTPPlugin();

    private Configuration _configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
    private Workstation workstation;
    private Statistics _statistics = new Statistics();
    #endregion

    #region Constructor

    public MainForm(string operatorLogin, Form LoginForm)
    {
        InitializeComponent();
        operatorName = operatorLogin;
        loginForm = LoginForm;
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
        operatorLoginLabel.Text = operatorName;
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

    private async Task ProcessLogFiles(List<string> logFiles)
    {
        try
        {
            foreach (var logFile in logFiles)
            {
                WaitForFileProcessed(logFile);
                LogFile LF = new(logFile)
                {
                    Operator = operatorName
                };
                if (SkippingNeeded(LF, logFile)) continue;
                LF = CheckFor3510RuleViolation(LF);
                if (SendOverHTTP) await _httpService.HttpPost(LF);
                if (SendOverFTP) await _ftpService.SendToFTP(logFile);
                if (CopyDir != String.Empty) CopyFile(logFile);
                MoveFile(logFile);
                UpdateStatistics(LF);
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

        void WaitForFileProcessed(string logFile)
        {
            // Wait until file is released
            for (int i = 0; i < 5; i++)
            {
                if (!IsFileLocked(new FileInfo(logFile)))
                    break;
                Thread.Sleep(100);
            }
        }

        bool SkippingNeeded(LogFile LF, string logFile)
        {
            // Filter data basing on sending option setting
            // Transfer only passed
            if (loggingOption == 0)
            {
                if (LF.Status != "Passed")
                {
                    File.Delete(logFile);
                    return true;
                }
            }
            // Delete all files and transfer nothing
            if (loggingOption == 1)
            {
                File.Delete(logFile);
                return true;
            }
            // Transfer all
            if (loggingOption == 2)
            { }
            return false;
        }

        LogFile CheckFor3510RuleViolation(LogFile LF)
        {
            var sameSNs = ProcessedData.Where(logFile => logFile.SerialNumber == LF.SerialNumber);
            if (sameSNs.Any()) LF.IsFirstPass = false;
            else LF.IsFirstPass = true;

            var failedSameSNs = sameSNs.Where(logFile => logFile.Status == "Failed" && logFile.TestDateTimeStarted >= DateTime.Now.AddMinutes(-15));
            if (failedSameSNs.Any() && LF.Status == "Passed") LF.FalseCall = true;
            else LF.FalseCall = false;

            var firstPass = ProcessedData.
                Where((logFile) => logFile.IsFirstPass == true);

            var rule3Check = firstPass.Skip(Math.Max(0, firstPass.Count() - 3));

            if (rule3Check.Where(logFile=>logFile.Status == "Failed").Count() >= 3)
            {
                // 3 violation
            }

            var rule5Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= DateTime.Now.AddHours(-1));

            if (rule5Check.Where(logFile=>logFile.Status == "Failed").Count() >= 5)
            {
                // 5 violation
            }
           
            var rule10Check = firstPass.Where(logFile => logFile.TestDateTimeStarted >= ProductionShift.CurrentShift.ShiftStart && logFile.TestDateTimeStarted < ProductionShift.CurrentShift.ShiftEnd);

            if (rule10Check.Where(logFile => logFile.Status == "Failed").Count() >= 10)
            {
                // 10 violation
            }

            return LF;
        }

        void UpdateStatistics(LogFile LF)
        {
            ProcessedData.Add(LF);
            _statistics.numberOfFilesProcessed++;
            if (LF.Status != "Passed")
                _statistics.numberOfFilesFailed++;
            statisticsControl1.UpdateStatistics(ref _statistics);
        }
    }

    private void CopyFile(string logFile)
    {
        var cdir = Path.Combine(CopyDir, $"{DateTime.Now.Year}_{DateTime.Now.Month.ToString("00")}");
        Directory.CreateDirectory(cdir);
        File.Copy(logFile, Path.Combine(cdir, Path.GetFileName(logFile)));
    }

    private void MoveFile(string logFile)
    {
        File.Move(logFile, Path.Combine(OutputDir, Path.GetFileName(logFile)), true);
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
    /// Asynchronous method for checking if test data is transferred correctly into MES.
    /// </summary>
    /// <param name="lF">Represents currently processed test data object.</param>
    /// <returns>Returned value is not used.</returns>
    private async Task<string> CheckMES(string state)
    {
        var dataToCheck = ProcessedData.Last();
        try
        {
            var alarm = true;
            await Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_mesService.TestDataPresentInSystem(dataToCheck))
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
                //dataLoggingSwitchButton.PerformClick();
                MessageBox.Show("Logi nie pojawiają się w systemie w ciągu 5 min. Wezwij technika.");
                state = "YellowAlert";
            }
            return state;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.StackTrace);
            MessageBox.Show(ex.Message);
            Close();
            return state;
        }
    }

    private void SyncWorkstation()
    {
        if (!SendOverHTTP) return;
        IEnumerable<Workstation> workstations = _httpService.HttpGet<Workstation>().Result.Where(x => x.Name == TestStationName);
        switch (workstations.Count())
        {
            case > 1:
                throw new Exception($"More than one workstation with name '{TestStationName}' exists!");
            case 1:
                workstation = workstations.First();
                break;
            case 0:
                workstation = _httpService.HttpPost<Workstation>(new Workstation(TestStationName)).Result.ReadFromJsonAsync<Workstation>().Result;
                break;
            default:
                throw new Exception("Error reading workstations.");
        }
    }

    private void CheckForProblems()
    {
        var state = "Running";
        if (VerifyMES) state = CheckMES(state).Result;
        if (Verify3510) state = Check3510(state).Result;
    }

    private async Task<string> Check3510(string state)
    {
        var firstPassData = ProcessedData.Where(logFile => logFile.IsFirstPass);
        if (firstPassData.Count() < 3) return state;

        for (int i = firstPassData.Count(); i > 0; i--)
        {
            firstPassData.ElementAt(Index.FromStart(i));
        }

        return state;
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
        SyncWorkstation();
        TurnOffLogging();
        TurnOnLogging();
        timer3000ms.Start();
    }

    // Main loop
    private void timer3000ms_Tick(object sender, EventArgs e)
    {
        if (dataLogging)
        {
            var logFiles = GetLogFiles();
            ProcessLogFiles(logFiles);
        }     
    }

    // Checks workstation status
    private void timer20min_Tick(object sender, EventArgs e)
    {
        var latestResults = ProcessedData.Where(logFile=>logFile.TestDateTimeStarted >= DateTime.Now.AddMinutes(-20));
        if (latestResults.Count() == 0) return;

        CheckForProblems();

        var response = _httpService.HttpPut<Workstation>(workstation).Result;
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
