using Microsoft.Extensions.Logging;
using ProductTest.Interfaces;
using System.Diagnostics;
using TestManager.Configuration;
using TestManager.Features.ProductionSupervision;
using TestManager.Features.Transporters;

namespace TestManager;

public partial class MainForm : Form
{
    private IWritableOptions<Config> _writableConfig;
    private IDirectoryConfig _directoryConfig;
    private IWebConfig _webConfig;

    private ILogger<MainForm> _logger;
    private IStatistics _statistics;
    private ITransporterFactory _transporterFactory;
    private IWorkstation _workstation;

    public MainForm(IWritableOptions<Config> writableConfig, 
                    IDirectoryConfig directoryConfig,
                    IWebConfig webConfig,
                    ILogger<MainForm> logger,
                    IStatistics statistics,
                    ITransporterFactory transporterFactory,
                    IWorkstation workstation)
    {
        InitializeComponent();

        _writableConfig = writableConfig;
        _directoryConfig = directoryConfig;
        _webConfig = webConfig;

        _logger = logger;
        _statistics = statistics;
        _transporterFactory = transporterFactory;
        _workstation = workstation;

        statisticsControl.Statistics = _statistics;
    }

    #region Buttons
    private void logOutButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void testReportTransferSwitchButton_Click(object sender, EventArgs e)
    {
        if (_transporterFactory.IsDataTransferEnabled)
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
        Pareto paretoForm = new Pareto(_statistics.GetProcessedData());
        paretoForm.ShowDialog();
    }

    private void detailsButton_Click(object sender, EventArgs e)
    {
        Details detailsForm = new Details(_statistics.GetProcessedData());
        detailsForm.ShowDialog();
    }

    private void breakdownButton_Click(object sender, EventArgs e)
    {
        TurnOffTestReportTransfer();
        MalfunctionReport malfForm = new MalfunctionReport(_workstation.OperatorName, _workstation.Name);
        malfForm.ShowDialog();
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
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = _directoryConfig.CopyDir,
            FileName = "explorer.exe"
        };
        Process.Start(startInfo);
    }

    private void ftpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = ftpToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.SendOverFTP = newValue);
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = mesToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.VerifyMES= newValue);
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = verify3510ToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.Verify3510= newValue);
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = httpToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.SendOverHTTP= newValue);
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
            _logger.LogError(ex, ex.ToString());
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
        
    }
    #endregion

    #region Private methods
    private void TurnOffTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "OFF";
        testReportTransferSwitchButton.BackColor = Color.Red;
        _transporterFactory.IsDataTransferEnabled = false;
        transferOptionCombobox.Visible = true;
        transferOptionCombobox.SelectedIndex = 0;
    }

    private void TurnOnTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "ON";
        testReportTransferSwitchButton.BackColor = Color.Green;
        _transporterFactory.IsDataTransferEnabled = true;
        transferOptionCombobox.Visible = false;
        _transporterFactory.TransferOption = transferOptionCombobox.SelectedIndex;
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
}