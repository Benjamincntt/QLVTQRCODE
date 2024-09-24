namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKeChiTietBackup
{
    public int KyKiemKeChiTietId { get; set; }

    public int? VatTuId { get; set; }

    public int? KhoChinhId { get; set; }

    public int? KhoPhuId { get; set; }

    public decimal? SoLuongSoSach { get; set; }

    public decimal? SoLuongKiemKe { get; set; }

    public decimal? SoLuongChenhLech { get; set; }

    public string? SoThe { get; set; }

    public int? TrangThai { get; set; }

    public DateTime? NgayKiemKe { get; set; }

    public int? NguoiKiemKeId { get; set; }

    public string? NguoiKiemKeTen { get; set; }

    public int? KyKiemKeBackupId { get; set; }

    public int? KiemKeIdGoc { get; set; }

    public virtual QlvtKyKiemKeBackup? KyKiemKeBackup { get; set; }

    public virtual ICollection<QlvtKyKiemKeChiTietDffBackup> QlvtKyKiemKeChiTietDffBackups { get; set; } = new List<QlvtKyKiemKeChiTietDffBackup>();
}
