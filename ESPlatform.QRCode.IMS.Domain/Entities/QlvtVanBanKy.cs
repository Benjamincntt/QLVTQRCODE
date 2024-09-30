namespace ESPlatform.QRCode.IMS.Domain.Entities;

public class QlvtVanBanKy
{
    public int Id { get; set; }

    public int? PhieuId { get; set; }

    public string? MaLoaiVanBan { get; set; }

    public string? FilePath { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? TrangThaiTaoFile { get; set; }

    public string? FileName { get; set; }

    public virtual QlvtMuaSamPhieuDeXuat? Phieu { get; set; }
}