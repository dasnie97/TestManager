using System.Diagnostics;
using TestManager.Interfaces;
using TestManager.Configuration;
using System.Reflection;

namespace TestManager;

public partial class MainForm : Form
{
    private IWebConfig _webConfig;
    private IDirectoryConfig _directoryConfig;
    private IAppSettingsWriter _appSettingsWriter;

    private IStatistics _statistics;
    private IProblemDetector _problemDetector;
    private ITransporterFactory _transporterFactory;
    private IWorkstation _workstation;

    public MainForm(IWebConfig webConfig,
        IDirectoryConfig directoryConfig,
        IAppSettingsWriter appSettingsWriter,
        IStatistics statistics,
        IProblemDetector problemDetector,
        ITransporterFactory transporterFactory,
        IWorkstationFactory workstationFactory)
    {
        InitializeComponent();

        _webConfig = webConfig;
        _directoryConfig = directoryConfig;
        _appSettingsWriter = appSettingsWriter;

        _statistics = statistics;
        _problemDetector = problemDetector;
        _transporterFactory = transporterFactory;
        _workstation = workstationFactory.CreateWorkstation();

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
        //TODO: PUT Workstation status
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
        _appSettingsWriter.UpdateAppSetting("WebConfig", "SendToFTP", newValue);
        _webConfig.ReadConfig();
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = mesToolStripMenuItem.Checked;
        _appSettingsWriter.UpdateAppSetting("WebConfig", "VerifyMES", newValue);
        _webConfig.ReadConfig();
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = verify3510ToolStripMenuItem.Checked;
        _appSettingsWriter.UpdateAppSetting("WebConfig", "Verify3510", newValue);
        _webConfig.ReadConfig();
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = httpToolStripMenuItem.Checked;
        _appSettingsWriter.UpdateAppSetting("WebConfig", "SendToWebAPI", newValue);
        _webConfig.ReadConfig();
    }

    #endregion

    #region Events

    private void MainForm_Load(object sender, EventArgs e)
    {
        LoadConfig();
        SyncWorkstation();
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
        _problemDetector.RunDetector();
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

        ftpToolStripMenuItem.Checked = _webConfig.SendToFTP;
        httpToolStripMenuItem.Checked = _webConfig.SendToWebAPI;
        mesToolStripMenuItem.Checked = _webConfig.VerifyMES;
        verify3510ToolStripMenuItem.Checked = _webConfig.Verify3510;

        inputToolStripMenuItem.Text = $"Input: {_directoryConfig.InputDir}";
        outputToolStripMenuItem.Text = $"Output: {_directoryConfig.OutputDir}";
        copyToolStripMenuItem.Text = $"Copy: {_directoryConfig.CopyDir}";

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        this.Text = $"TestManager v{version.Major}.{version.Minor}.{version.Build}";
    }

    private async void SyncWorkstation()
    {
        await _workstation.SyncWorkstation();
    }

    #endregion
}