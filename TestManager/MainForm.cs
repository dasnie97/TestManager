using System.Diagnostics;
using TestManager.Helpers;
using ProductTest.Models;

namespace TestManager;

public partial class MainForm : Form
{
    private Form _loginForm;
    private ConfigHandler _config;
    private FileProcessor _fileProcessor;
    private Statistics _statistics;
    private Workstation _workstation;

    public MainForm(string operatorLogin, Form loginForm)
    {
        InitializeComponent();
        _config = new ConfigHandler();
        _statistics = new Statistics();
        _fileProcessor = new FileProcessor(_config, _statistics);
        _workstation = new Workstation(_config.TestStationName, operatorLogin);
        _loginForm = loginForm;
    }

    #region Buttons
    private void logOutButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void testReportTransferSwitchButton_Click(object sender, EventArgs e)
    {
        if (_fileProcessor.IsDataTransferEnabled)
        {
            TurnOffTestReportTransfer();
        }
        else
        {
            TurnOnTestReportTransfer();
        }
    }

    private void topFailuresButton_Click(object sender, EventArgs e)
    {
        try
        {
            Pareto paretoForm = new Pareto(_fileProcessor.ProcessedData);
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
            Details detailsForm = new Details(_fileProcessor.ProcessedData);
            detailsForm.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void breakdownButton_Click(object sender, EventArgs e)
    {
        TurnOffTestReportTransfer();
        try
        {
            MalfunctionReport malfForm = new MalfunctionReport(_workstation.OperatorName, _config.TestStationName);
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
            Arguments = _config.InputDir,
            FileName = "explorer.exe"
        };
        Process.Start(startInfo);
    }

    private void outputToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = _config.OutputDir,
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
                Arguments = _config.CopyDir,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
        catch { }
    }

    private void ftpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(ftpToolStripMenuItem.Checked);
        _config.WriteConfig("SendOverFTP", newValue);
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(mesToolStripMenuItem.Checked);
        _config.WriteConfig("VerifyMES", newValue);
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(verify3510ToolStripMenuItem.Checked);
        _config.WriteConfig("Verify3510", newValue);
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(httpToolStripMenuItem.Checked);
        _config.WriteConfig("SendOverHTTP", newValue);
    }

    #endregion

    #region Private methods
    private void TurnOffTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "OFF";
        testReportTransferSwitchButton.BackColor = Color.Red;
        _fileProcessor.IsDataTransferEnabled = false;
        transferOptionCombobox.Visible = true;
        transferOptionCombobox.SelectedIndex = 0;
    }

    private void TurnOnTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "ON";
        testReportTransferSwitchButton.BackColor = Color.Green;
        _fileProcessor.IsDataTransferEnabled = true;
        transferOptionCombobox.Visible = false;
        _fileProcessor.TransferOption = transferOptionCombobox.SelectedIndex;
    }

    private void LoadConfig()
    {
        stationNameLabel.Text = _config.TestStationName;
        operatorLoginLabel.Text = _workstation.OperatorName;
        ftpToolStripMenuItem.Checked = _config.SendOverFTP;
        httpToolStripMenuItem.Checked = _config.SendOverHTTP;
        mesToolStripMenuItem.Checked = _config.VerifyMES;
        verify3510ToolStripMenuItem.Checked = _config.Verify3510;
        inputToolStripMenuItem.Text = $"Input: {_config.InputDir}";
        outputToolStripMenuItem.Text = $"Output: {_config.OutputDir}";
        copyToolStripMenuItem.Text = $"Copy: {_config.CopyDir}";
    }
    #endregion

    #region Form events & timers
    private void MainForm_Load(object sender, EventArgs e)
    {
        try
        {
            LoadConfig();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            Close();
        }
    }

    private void timer3000ms_Tick(object sender, EventArgs e)
    {
        _fileProcessor.ProcessFiles();
        statisticsControl.UpdateStatistics(_statistics);
    }

    private void timer20min_Tick(object sender, EventArgs e)
    {
        
    }

    private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        // Commented to ommit operator login - feature not released yet
        //loginForm.Show();
    }
    #endregion
}