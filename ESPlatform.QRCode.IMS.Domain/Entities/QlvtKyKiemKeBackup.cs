namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKeBackup
{
    public int Id { get; set; }

    public int? OrganizationId { get; set; }

    public string? OrganizationCode { get; set; }

    public int PhysicalInventoryId { get; set; }

    public string? PhysicalInventoryName { get; set; }

    public string? Description { get; set; }

    public DateTime? PhysicalInventoryDate { get; set; }

    public DateTime? FreezeDate { get; set; }

    public string? StartTag { get; set; }

    public string? EndTag { get; set; }

    public string? UserName { get; set; }

    public short? Kykiemkechinh { get; set; }

    public int? KiemKeIdGoc { get; set; }

    public DateTime? NgaySaoLuu { get; set; }

    public long BackupId { get; set; }
}