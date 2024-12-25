namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TempKyKiemKe
{
    public int IdKho { get; set; }

    public int? IdDonvi { get; set; }

    public int IdKy { get; set; }

    public string TenKy { get; set; } = null!;

    public string MaKho { get; set; } = null!;

    public string TenKho { get; set; } = null!;

    public string DienGiai { get; set; } = null!;

    public DateTime? NgayKiemKe { get; set; }

    public DateTime? NgayKhoa { get; set; }

    public string SoTheDauTien { get; set; } = null!;

    public string SoTheKetThuc { get; set; } = null!;

    public string NguoiThucHien { get; set; } = null!;
}