using System.Text.RegularExpressions;
using System.Configuration;

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

            if (!Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AutoLogIn")))
                Application.Run(new LogIn());
            else
                Application.Run(new MainForm("operat", new Form()));
        }
    }
}