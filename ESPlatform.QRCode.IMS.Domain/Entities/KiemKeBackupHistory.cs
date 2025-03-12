namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class KiemKeBackupHistory
{
    public long BackupId { get; set; }

    public int PhysicalInventoryId { get; set; }

    public DateTime BackupTime { get; set; }

    public string? BackupReason { get; set; }

    public int? RecordsCount { get; set; }

    public string? Status { get; set; }

    public string? ErrorMessage { get; set; }
}
