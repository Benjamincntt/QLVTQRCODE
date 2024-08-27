namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVtHop
{
    public int Id { get; set; }

    public string? TenHop { get; set; }

    public string? MaHop { get; set; }

    public int? IdtoMay { get; set; }

    public int? IdgiaKe { get; set; }

    public int? Idngan { get; set; }

    public string? KichThuoc { get; set; }

    public string? MoTa { get; set; }

    public int? MaDonViSuDung { get; set; }

    public byte? TrangThai { get; set; }

    public DateTime? NgayThem { get; set; }

    public int? MaNguoiThem { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiSua { get; set; }
}
