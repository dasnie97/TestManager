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

    private IStatistics _statistics;
    private ITransporterFactory _transporterFactory;
    private IWorkstation _workstation;

    public MainForm(IWritableOptions<Config> writableConfig, 
                    IDirectoryConfig directoryConfig,
                    IWebConfig webConfig,
                    IStatistics statistics,
                    ITransporterFactory transporterFactory,
                    IWorkstation workstation)
    {
        InitializeComponent();

        _writableConfig = writableConfig;
        _directoryConfig = directoryConfig;
        _webConfig = webConfig;

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
        ParetoForm paretoForm = new ParetoForm(_statistics.GetProcessedData());
        paretoForm.ShowDialog();
    }

    private void detailsButton_Click(object sender, EventArgs e)
    {
        DetailsForm detailsForm = new DetailsForm(_statistics.GetProcessedData());
        detailsForm.ShowDialog();
    }

    private void breakdownButton_Click(object sender, EventArgs e)
    {
        TurnOffTestReportTransfer();
        MalfunctionReportForm malfForm = new MalfunctionReportForm(_workstation);
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
        _webConfig.ReadConfig();
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = mesToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.VerifyMES= newValue);
        _webConfig.ReadConfig();
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = verify3510ToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.Verify3510= newValue);
        _webConfig.ReadConfig();
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = httpToolStripMenuItem.Checked;
        _writableConfig.Update(cfg => cfg.SendOverHTTP= newValue);
        _webConfig.ReadConfig();
    }

    #endregion

    #region Events

    private void MainForm_Load(object sender, EventArgs e)
    {
        LoadConfig();
    }

    private async void timer3000ms_Tick(object sender, EventArgs e)
    {
        timer3000ms.Stop();

        await RunFileProcessing();

        timer3000ms.Start();
    }

    private void Transporter_FileTransported(object? sender, EventArgs e)
    {
        statisticsControl.UpdateStatistics();
    }

    #endregion

    #region Private methods

    private async Task RunFileProcessing()
    {
        var transporter = _transporterFactory.GetTransporter();
        transporter.FileTransported += Transporter_FileTransported;
        await Task.Run(() => transporter.TransportTestReports());
    }

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