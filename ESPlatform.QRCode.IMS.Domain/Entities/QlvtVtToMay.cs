namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVtToMay
{
    public int Id { get; set; }

    public string? TenToMay { get; set; }

    public string? MaToMay { get; set; }

    public int? IdkhoErp { get; set; }

    public string? KichThuoc { get; set; }

    public string? MoTa { get; set; }

    public int? MaDonViSuDung { get; set; }

    public byte? TrangThai { get; set; }

    public DateTime? NgayThem { get; set; }

    public int? MaNguoiThem { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiSua { get; set; }
}
