using System.Text.RegularExpressions;

namespace TestManager;

public partial class LogInForm : Form
{
    private bool formValidationPassed;

    public LogInForm()
    {
        InitializeComponent();
    }

    private void acceptButton_Click(object sender, EventArgs e)
    {
        ValidateForm();

        if (formValidationPassed)
        {
            operatorLoginTextbox.Clear();
            Hide();
            //TODO: Create and display MainForm
        }
    }

    private void ValidateForm()
    {
        formValidationPassed = true;
        string operatorLogin = operatorLoginTextbox.Text.Trim().ToLower();

        if (operatorLogin.Length != 6)
        {
            SetValidationFailed("Login powinien składać się z 6 znaków - dwie pierwsze litery imienia oraz cztery pierwsze litery nazwiska!");
            return;
        }

        Regex regex = new Regex(@"[a-z]");
        MatchCollection matches = regex.Matches(operatorLogin);

        if (matches.Count != 6)
        {
            SetValidationFailed("Login powinien zawierać tylko litery bez polskich znaków!");
            return;
        }

    }

    private void SetValidationFailed(string textToDisplay)
    {
        MessageBox.Show(textToDisplay);
        operatorLoginTextbox.Clear();
        formValidationPassed = false;
    }

    private void operatorLoginTextbox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            acceptButton.PerformClick();
        }
    }

    private void LoginForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Down)
        {
            Close();
        }
    }
}
