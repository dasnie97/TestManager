using System.Text.RegularExpressions;
using TestManager.Web;

namespace TestManager;

public partial class MalfunctionReportForm : Form
{
    public DateTime BreakdownStarted;
    public TimeSpan BreakdownTimeSpan;

    private readonly IWorkstation _workstation;
    private bool formValidationPassed;

    public MalfunctionReportForm(IWorkstation workstation)
    {
        InitializeComponent();
        _workstation = workstation;
        BreakdownStarted = DateTime.Now;
        timer1.Start();
    }

    private void sendReportButton_Click(object sender, EventArgs e)
    {
        ValidateForm();

        if (formValidationPassed)
        {
            CalculateBreakdownTimeSpan();
            // TODO: Send report to DB
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

        if (string.IsNullOrEmpty(descriptionComboBox.Text))
        {
            SetValidationFailed("Wprowadź opis awarii!");
            return;
        }

        if (string.IsNullOrEmpty(actionTakenComboBox.Text))
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

    private void MalfunctionReport_Shown(object sender, EventArgs e)
    {
        technicianNamesComboBox.Focus();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        TimeSpan timePassedFromStart = DateTime.Now - BreakdownStarted;
        breakdownTimeStartedLabel.Text = timePassedFromStart.ToString().Substring(0, 8);
    }
}
