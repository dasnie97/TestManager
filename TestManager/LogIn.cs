using System.Text.RegularExpressions;

namespace TestManager;

public partial class LogIn : Form
{
    private MainForm mainForm;

    public LogIn()
    {
        InitializeComponent();
    }

    private void loginTextbox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            this.acceptButton.PerformClick();
        }
    }

    private void LoginForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Down)
        {
            Close();
            mainForm?.Close();
        }
    }

    private void acceptButton_Click_1(object sender, EventArgs e)
    {
        string operatorLogin = loginTextbox.Text.Trim().ToLower();

        if (operatorLogin.Length != 6)
        {
            MessageBox.Show("Login powinien składać się z 6 znaków - dwie pierwsze litery imienia oraz cztery pierwsze litery nazwiska!");
            loginTextbox.Clear();
            return;
        }

        if (Regex.Matches(operatorLogin, @"[a-z]").Count != 6)
        {
            MessageBox.Show("Login powinien zawierać tylko litery bez polskich znaków!");
            loginTextbox.Clear();
            return;
        }

        loginTextbox.Clear();
        Hide();
        mainForm = new MainForm(operatorLogin, FindForm());
        mainForm.Show();
    }
}
