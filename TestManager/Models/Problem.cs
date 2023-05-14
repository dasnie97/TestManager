namespace TestManager.Models;

public class Problem
{
    public static readonly string RedAlert = "RedAlert";
    public static readonly string OrangeAlert = "OrangeAlert";

    public Problem(string alert, string message = "")
    {
        Alert = alert;
        Message = message;
    }
    public string Alert { get; private set; }
    public string Message { get; private set; }

}
