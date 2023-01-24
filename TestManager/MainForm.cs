using System.Diagnostics;
using TestManager.Helpers;
using TestManager.Transporters;

namespace TestManager;

public partial class MainForm : Form
{
    private Form _loginForm;
    private TransporterFactory _transporterFactory;
    private WebAdapter _webAdapter;
    private Workstation _workstation;

    public MainForm(string operatorLogin, Form loginForm)
    {
        InitializeComponent();
        _transporterFactory = new TransporterFactory();
        _loginForm = loginForm;
        _workstation = new Workstation(Config.Instance.TestStationName, operatorLogin);
    }

    #region Buttons
    private void logOutButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void testReportTransferSwitchButton_Click(object sender, EventArgs e)
    {
        if (FileProcessor.Instance.IsDataTransferEnabled)
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
            Pareto paretoForm = new Pareto(FileProcessor.Instance.ProcessedData);
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
            Details detailsForm = new Details(FileProcessor.Instance.ProcessedData);
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
            Arguments = Config.Instance.InputDir,
            FileName = "explorer.exe"
        };
        Process.Start(startInfo);
    }

    private void outputToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            Arguments = Config.Instance.OutputDir,
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
                Arguments = Config.Instance.CopyDir,
                FileName = "explorer.exe"
            };
            Process.Start(startInfo);
        }
        catch { }
    }

    private void ftpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(ftpToolStripMenuItem.Checked);
        Config.Instance.WriteConfig("SendOverFTP", newValue);
    }

    private void mesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(mesToolStripMenuItem.Checked);
        Config.Instance.WriteConfig("VerifyMES", newValue);
    }

    private void verify3510ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(verify3510ToolStripMenuItem.Checked);
        Config.Instance.WriteConfig("Verify3510", newValue);
    }

    private void httpToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var newValue = Convert.ToString(httpToolStripMenuItem.Checked);
        Config.Instance.WriteConfig("SendOverHTTP", newValue);
    }

    #endregion

    #region Private methods
    private void TurnOffTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "OFF";
        testReportTransferSwitchButton.BackColor = Color.Red;
        FileProcessor.Instance.IsDataTransferEnabled = false;
        transferOptionCombobox.Visible = true;
        transferOptionCombobox.SelectedIndex = 0;
    }

    private void TurnOnTestReportTransfer()
    {
        testReportTransferSwitchButton.Text = "ON";
        testReportTransferSwitchButton.BackColor = Color.Green;
        FileProcessor.Instance.IsDataTransferEnabled = true;
        transferOptionCombobox.Visible = false;
        FileProcessor.Instance.TransferOption = transferOptionCombobox.SelectedIndex;
    }

    private void LoadConfig()
    {
        stationNameLabel.Text = _workstation.Name;
        operatorLoginLabel.Text = _workstation.OperatorName;
        ftpToolStripMenuItem.Checked = Config.Instance.SendOverFTP;
        httpToolStripMenuItem.Checked = Config.Instance.SendOverHTTP;
        mesToolStripMenuItem.Checked = Config.Instance.VerifyMES;
        verify3510ToolStripMenuItem.Checked = Config.Instance.Verify3510;
        inputToolStripMenuItem.Text = $"Input: {Config.Instance.InputDir}";
        outputToolStripMenuItem.Text = $"Output: {Config.Instance.OutputDir}";
        copyToolStripMenuItem.Text = $"Copy: {Config.Instance.CopyDir}";
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