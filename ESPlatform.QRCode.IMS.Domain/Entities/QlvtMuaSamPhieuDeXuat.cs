namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtMuaSamPhieuDeXuat
{
    public int Id { get; set; }

    public string? MaPhieu { get; set; }

    public string? TenPhieu { get; set; }

    public string? MoTa { get; set; }

    public int? MaDonViSuDung { get; set; }

    public DateTime? NgayThem { get; set; }

    public int? MaNguoiThem { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiSua { get; set; }

    public byte? TrangThai { get; set; }

    public int? IdcanBoDuyet { get; set; }

    public string? CanBoLyDo { get; set; }

    public string? CanBoFileKy { get; set; }

    public int? IdlanhDaoDuyet { get; set; }

    public string? LanhDaoLyDo { get; set; }

    public string? LanhDaoFileKy { get; set; }
}
