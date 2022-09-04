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

            var operatorName = LoadDefaultOperator();

            if (operatorName == String.Empty)
                Application.Run(new LogIn());
            else
                Application.Run(new MainForm(operatorName, new Form()));
        }

        /// <summary>
        /// Load default operator name from txt config file located in same folder as exe.
        /// </summary>
        static string LoadDefaultOperator()
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