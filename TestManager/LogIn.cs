using System.Text.RegularExpressions;

namespace TestManager
{
    /// <summary>
    /// Login form for logging name of operator working on test station.
    /// </summary>
    public partial class LogIn : Form
    {
        // Create MainForm field for MainForm object. This way it is possible to close both forms if needed.
        private MainForm mainForm;

        /// <summary>
        /// Default constructor
        /// </summary>
        public LogIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Make it possible to login using enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.acceptButton.PerformClick();
            }
        }

        /// <summary>
        /// Secret combination of keys to close both login form and downtime form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Down)
            {
                this.Close();

                // Make sure MainForm field is not NULL and close the form it is not
                mainForm?.Close();
            }
        }

        /// <summary>
        /// Login button. Check if operator has entered valid login. TODO: Problem: any 6 latin characters are acceptable.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void acceptButton_Click_1(object sender, EventArgs e)
        {
            // Remove white spaces and set letters to lower case
            string operatorLogin = this.loginTextbox.Text.Trim().ToLower();

            // Check if entered text has correct number of characters
            if (operatorLogin.Length != 6)
            {
                MessageBox.Show("Login powinien składać się z 6 znaków - dwie pierwsze litery imienia oraz cztery pierwsze litery nazwiska!");
                this.loginTextbox.Clear();
                return;
            }

            // Check if entered text contains only allowed characters (Latin a-z)
            if (Regex.Matches(operatorLogin, @"[a-z]").Count != 6)
            {
                MessageBox.Show("Login powinien zawierać tylko litery bez polskich znaków!");
                this.loginTextbox.Clear();
                return;
            }

            this.loginTextbox.Clear();

            // Set visible property of this form to false
            this.Hide();

            // Create Downtime form and pass operator login text and this form to it
            mainForm = new MainForm(operatorLogin, this.FindForm());
            mainForm.Show();
        }
    }
}
