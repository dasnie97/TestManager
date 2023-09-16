using System.Text.RegularExpressions;
using TestManager.Interfaces;
using TestManager.Models;

namespace TestManager;

public partial class DowntimeReportForm : Form
{
    public DateTime BreakdownStarted;
    public TimeSpan BreakdownTimeSpan;

    private IWorkstation _workstation;
    private readonly IWebAdapter _webAdapter;
    private bool formValidationPassed;

    public DowntimeReportForm(IWorkstationFactory workstationFactory, IWebAdapter webAdapter)
    {
        InitializeComponent();
        _workstation = workstationFactory.CreateWorkstation();
        _webAdapter = webAdapter;

        BreakdownStarted = DateTime.Now;
        timer1.Start();
    }

    private async void sendReportButton_Click(object sender, EventArgs e)
    {
        ValidateForm();

        if (formValidationPassed)
        {
            CalculateBreakdownTimeSpan();
            DowntimeReport downtimeReport = new DowntimeReport();
            downtimeReport.ProblemDescription = descriptionComboBox.Text + "\n" + descriptionTextbox.Text;
            downtimeReport.ActionTaken = actionTakenComboBox.Text + "\n" + actionTextbox.Text;
            downtimeReport.Technician = technicianNamesComboBox.Text;
            downtimeReport.Workstation = _workstation.Name;
            downtimeReport.Operator = _workstation.OperatorName;
            downtimeReport.TimeStarted = BreakdownStarted;
            downtimeReport.TimeFinished = DateTime.Now;
            downtimeReport.TotalDowntime = new TimeSpan(BreakdownTimeSpan.Hours, BreakdownTimeSpan.Minutes, BreakdownTimeSpan.Seconds);

            await _webAdapter.HTTPPost(downtimeReport);
            Close();
        }
    }

    private void ValidateForm()
    {
        formValidationPassed = true;

        if (string.IsNullOrEmpty(technicianNamesComboBox.Text))
        {
            SetValidationFailed("Wprowadź swój login!");
            return;
        }

        if (string.IsNullOrEmpty(descriptionComboBox.Text) && string.IsNullOrEmpty(descriptionTextbox.Text))
        {
            SetValidationFailed("Wprowadź opis awarii!");
            return;
        }

        if (string.IsNullOrEmpty(actionTakenComboBox.Text) && string.IsNullOrEmpty(actionTextbox.Text))
        {
            SetValidationFailed("Wprowadź opis podjętych działań!");
            return;
        }

        if (!string.IsNullOrEmpty(malfunctionTimeOptionalTextBox.Text))
        {
            string userInputTime = malfunctionTimeOptionalTextBox.Text.Trim();

            Regex r = new Regex(@"\d{1,3}");
            Match m = r.Match(userInputTime);

            if (!m.Success || userInputTime.Length > 3 || m.Length != userInputTime.Length)
            {
                SetValidationFailed("Wprowadź odpowiednią ilość minut do opcjonalnego pola czasu trwania awarii!");
                malfunctionTimeOptionalTextBox.Clear();
                return;
            }
        }
    }

    private void SetValidationFailed(string textToDisplay)
    {
        formValidationPassed = false;
        MessageBox.Show(textToDisplay);
    }

    private void CalculateBreakdownTimeSpan()
    {
        if (string.IsNullOrEmpty(malfunctionTimeOptionalTextBox.Text))
        {
            BreakdownTimeSpan = DateTime.Now - BreakdownStarted;
        }
        else
        {
            string minutes = malfunctionTimeOptionalTextBox.Text.Trim();
            float timeSpan = float.Parse(minutes);
            BreakdownTimeSpan = TimeSpan.FromMinutes(timeSpan);
        }
    }

    private void DowntimeReport_Shown(object sender, EventArgs e)
    {
        technicianNamesComboBox.Focus();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        TimeSpan timePassedFromStart = DateTime.Now - BreakdownStarted;
        breakdownTimeStartedLabel.Text = timePassedFromStart.ToString().Substring(0, 8);
    }
}
