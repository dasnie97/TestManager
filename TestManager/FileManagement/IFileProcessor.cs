using ProductTest.Models;

namespace TestManager.FileManagement;

public interface IFileProcessor
{
    public IEnumerable<FileTestReport> LoadFiles();
    public string MoveFile(FileTestReport testReport);
    public string CopyFile(FileTestReport testReport);
    public void DeleteFile(FileTestReport testReport);
}
