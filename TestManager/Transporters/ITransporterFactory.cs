namespace TestManager.Transporters;

public interface ITransporterFactory
{
    public bool IsDataTransferEnabled { get; set; }
    public int TransferOption { get; set; }
    public ITransporter GetTransporter();
}