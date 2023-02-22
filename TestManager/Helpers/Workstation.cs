using ProductTest.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.Helpers;

public class Workstation : IWorkstation
{

	public string Name { get ; set; }
	public string OperatorName { get ; set; }

	public Workstation(string name = "", string operatorName = "")
	{
		Name = name;
		OperatorName = operatorName;
	}
}
