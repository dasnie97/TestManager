using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManager.ConfigHelpers;

public interface IDirectoryConfig
{
    public string InputDir { get; }
    public string OutputDir { get; }
    public string CopyDir { get; }
    public bool IsCopyingEnabled { get; }
    public string DateNamedCopyDirectory { get; }
    public void WriteConfig(string key, string value);
}