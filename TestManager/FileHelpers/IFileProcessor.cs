﻿using ProductTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManager.Helpers;

namespace TestManager.FileHelpers;

public interface IFileProcessor
{
    public bool IsDataTransferEnabled { get; set; }
    public int TransferOption { get; set; }
    public IEnumerable<FileTestReport> LoadFiles();
    public string MoveFile(FileTestReport testReport);
    public string CopyFile(FileTestReport testReport);
    public void DeleteFile(FileTestReport testReport);
    public void Reset();
}
