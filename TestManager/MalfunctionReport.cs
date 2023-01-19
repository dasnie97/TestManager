using System.Text.RegularExpressions;

namespace TestManager;

public partial class MalfunctionReport : Form
{
    public DateTime BreakdownStarted;
    public TimeSpan optionalBreakdownTimeInterval;
    //private HTTPPlugin<LogFile> _httpService = new();

    public MalfunctionReport(string OperatorLogin, string StationName)
    {
        InitializeComponent();
        BreakdownStarted = DateTime.Now;
        timer1.Start();
        //_httpService.UpdateTeststations(problemOperator: "1");
    }

    private void confirmButton_Click(object sender, EventArgs e)
    {
        if (technicianComboBox.Text.Length == 0)
        {
            MessageBox.Show("Wprowadź swój login!");
            return;
        }

        if (descriptionComboBox.Text.Length == 0 || actionTakenComboBox.Text.Length == 0)
        {
            MessageBox.Show("Wprowadź opis awarii oraz podjęte działania!");
            return;
        }

        if (malfunctionTimeOptionalTextBox.Text.Length != 0)
        {
            var minutes = malfunctionTimeOptionalTextBox.Text.Trim();

            Regex r = new Regex(@"\d{1,3}");
            Match m = r.Match(minutes);

            if (!m.Success || minutes.Length > 3)
            {
                MessageBox.Show("Wprowadź odpowiednią ilość minut do opcjonalnego pola czasu trwania awarii!");
                malfunctionTimeOptionalTextBox.Clear();
                return;
            }

            try
            {
                var timeSpan = float.Parse(minutes);
                optionalBreakdownTimeInterval = TimeSpan.FromMinutes(timeSpan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        // Decide if malfunction time should be caluclated basing on timer or optional textbox input
        //if (optionalBreakdownTimeInterval == new TimeSpan())
        //{
        //    sqlHandle.CrashStarted = BreakdownStarted.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    sqlHandle.CrashTime = (DateTime.Now - BreakdownStarted).ToString("hh\\:mm\\:ss");
        //}
        //else
        //{
        //    sqlHandle.CrashStarted = (DateTime.Now - optionalBreakdownTimeInterval).ToString("yyyy-MM-dd HH:mm:ss.fff");
        //    sqlHandle.CrashTime = optionalBreakdownTimeInterval.ToString("hh\\:mm\\:ss");
        //}
        //sqlHandle.ProblemDescription = descriptionComboBox.Text + " " + descriptionTextbox.Text;
        //sqlHandle.ActionTaken = actionTakenComboBox.Text + " " + actionTextbox.Text;
        //sqlHandle.Technician = technicianComboBox.Text;
        //sqlHandle.InsertCrashlog();
        Close();
    }

    private void MalfunctionReport_Shown(object sender, EventArgs e)
    {
        technicianComboBox.Focus();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        breakdownTimeStartedLabel.Text = (DateTime.Now - BreakdownStarted).ToString().Substring(0, 8);
    }

    private void MalfunctionReport_FormClosed(object sender, FormClosedEventArgs e)
    {
        //sqlHandle.UpdateTeststations(problemOperator: "0");
    }
}
