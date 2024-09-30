namespace ESPlatform.QRCode.IMS.Domain.Entities;

public class QlvtPhieuTrangThai
{
    public int Id { get; set; }

    public int? PhieuId { get; set; }

    public string? FilePath { get; set; }

    public bool? TrangThai { get; set; }

    public virtual QlvtMuaSamPhieuDeXuat? Phieu { get; set; }
}