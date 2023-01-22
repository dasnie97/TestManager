using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public class Problem
{
	public static readonly string RedAlert = "RedAlert";
    public static readonly string OrangeAlert = "OrangeAlert";

    public Problem(string alert, string message="")
	{
        Alert = alert;
        Message = message;   
	}
    public string Alert { get; private set; }
    public string Message { get; private set; }

}
