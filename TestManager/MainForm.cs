using System.Diagnostics;
using TestManager.ConfigHelpers;
using TestManager.FileHelpers;
using TestManager.Helpers;
using TestManager.Transporters;

namespace TestManager;

public partial class MainForm : Form
{
    private Form _loginForm;
    private TransporterFactory _transporterFactory;
    private WebAdapter _webAdapter;
    private Workstation _workstation;
    private IDirectoryConfig _directoryConfig;
    private IWorkstationConfig _workstationConfig;
    private IWebConfig _webConfig;
    private IFileProcessor _fileProcessor;
    private IStatistics _statistics;

    public MainForm(string operatorLogin, Form loginForm)
    {
        InitializeComponent();
        _loginForm = loginForm;
        _workstationConfig = Config.GetInstance();
        _directoryConfig = Config.GetInstance();
        _webConfig = Config.GetInstance();
        _statistics = Statistics.GetInstance();
        _fileProcessor = FileProcessor.GetInstance(_directoryConfig);
        _transporterFactory = new TransporterFactory(_fileProcessor, _statistics);
        _workstation = new Workstation(_workstationConfig.TestStationName, operatorLogin);
        statisticsControl.Statistics = _statistics;
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
            Pareto paretoForm = new Pareto(_statistics.ProcessedData);
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
            Details detailsForm = new Details(_statistics.ProcessedData);
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
            MalfunctionReport malfForm = new MalfunctionReport(_workstation.OperatorName, _workstation.Name);
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
            Arguments = _directoryConfig.InputDir,
            FileName = "explorer.exe"
        };
        Process.Start(startInfo);
    }

    private void outputToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = _directoryConfig.OutputDir,
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
                Arguments = _directoryConfig.CopyDir,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
        catch { }
    }

    private void ftpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(ftpToolStripMenuItem.Checked);
        _webConfig.WriteConfig("SendOverFTP", newValue);
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(mesToolStripMenuItem.Checked);
        _webConfig.WriteConfig("VerifyMES", newValue);
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(verify3510ToolStripMenuItem.Checked);
        _webConfig.WriteConfig("Verify3510", newValue);
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(httpToolStripMenuItem.Checked);
        _webConfig.WriteConfig("SendOverHTTP", newValue);
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
        stationNameLabel.Text = _workstation.Name;
        operatorLoginLabel.Text = _workstation.OperatorName;
        ftpToolStripMenuItem.Checked = _webConfig.SendOverFTP;
        httpToolStripMenuItem.Checked = _webConfig.SendOverHTTP;
        mesToolStripMenuItem.Checked = _webConfig.VerifyMES;
        verify3510ToolStripMenuItem.Checked = _webConfig.Verify3510;
        inputToolStripMenuItem.Text = $"Input: {_directoryConfig.InputDir}";
        outputToolStripMenuItem.Text = $"Output: {_directoryConfig.OutputDir}";
        copyToolStripMenuItem.Text = $"Copy: {_directoryConfig.CopyDir}";
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
        timer3000ms.Stop();

        var concreteTransporter = _transporterFactory.GetTransporter();
        concreteTransporter.TransportTestReports();
        statisticsControl.UpdateStatistics();

        timer3000ms.Start();
    }

    private void timer20min_Tick(object sender, EventArgs e)
    {
        
    }

    private void DowntimeForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        // Ommit operator login
        //loginForm.Show();
    }
    #endregion
}