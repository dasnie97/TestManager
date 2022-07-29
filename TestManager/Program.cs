// Assumptions - this app is runinng on test PC. It is started with 'ygrdtest01' user log on.

// In background, it archivies test log files on local disk.

// Operator logs in with his short login before starting work. On shift finish, operator logs out.

// If something is wrong with tester, technician is alarmed and comes to see whats the problem. After fixing it, he reports what was the cause and
// action he took. Application provides interface, where technician can enter details about intervention. It is then sent to data base.

// Application sends test log files to factory data base and test data base in real time.

// Application monitors factory data base to see if test results are present in real time. If something is wrong, application blocks work and rise alarm.

using System.Text.RegularExpressions;

namespace TestManager
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var operatorName = LoadConfig();

            if (operatorName == String.Empty)
                Application.Run(new LogIn());
            else
                Application.Run(new MainForm(operatorName, new Form()));
        }

        /// <summary>
        /// Get station name from txt file located in same folder as exe 
        /// </summary>
        static string LoadConfig()
        {
            var configPath = "config.txt";
            var value = "";

            if (!File.Exists(configPath))
            {
                MessageBox.Show($"Path {configPath} does not exist!");
                Environment.Exit(0);
            }

            foreach (var line in File.ReadLines(configPath))
            {
                if (line.StartsWith("Operator"))
                {
                    value = line.Split("\t")[1].Trim().ToLower();
                    break;
                }
                return "";
            }

            // Check if entered text has correct number of characters
            if (value.Length != 6)
            {
                MessageBox.Show("Login operatora powinien sk³adaæ siê z 6 znaków!");
                Environment.Exit(0);
            }

            // Check if entered text contains only allowed characters (Latin a-z)
            if (Regex.Matches(value, @"[a-z]").Count != 6)
            {
                MessageBox.Show("Login powinien zawieraæ tylko litery bez polskich znaków!");
                Environment.Exit(0);
            }

            return value;
        }
    }
}