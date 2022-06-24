//TODO:
// Assumptions - this app is runinng on test PC. It is started with 'ygrdtest01' user log on.

// In background, it archivies test log files on local disk.

// Operator logs in with his short login before starting work. On shift finish, operator logs out.

// If something is wrong with tester, technician is alarmed and comes to see whats the problem. After fixing it, he reports what was the cause and
// action he took. Application provides interface, where technician can enter details about intervention. It is then sent to data base.

// Application sends test log files to factory data base and test data base in real time.

// Application monitors factory data base to see if test results are present in real time. If something is wrong, application blocks work and rise alarm.


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

            // Changed to ommit operator login
            Application.Run(new MainForm("kitron", new Form()));
            //Application.Run(new MalfunctionReport());
        }
    }
}