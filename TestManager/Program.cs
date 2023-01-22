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
            if (ConfigFileIsPresent())
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AutoLogIn")))
                    //Application.Run(new LogIn());
                    Application.Run(new MainForm("operat", new Form()));
                else
                    Application.Run(new MainForm("operat", new Form()));
            }
            else
            {
                MessageBox.Show("Cannot find config file!");
                Environment.Exit(0);
            }
            
        }
        public static bool ConfigFileIsPresent()
        {
            var pathToAppConfig = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "TestManager.dll.config");
            return File.Exists(pathToAppConfig);
        }
    }
}