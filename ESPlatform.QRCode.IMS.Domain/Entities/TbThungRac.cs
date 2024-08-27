namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbThungRac
{
    public int MaThungRac { get; set; }

    public string? TieuDe { get; set; }

    /// <summary>
    /// Mã nội dung xóa
    /// </summary>
    public int? MaNoiDung { get; set; }

    /// <summary>
    /// vị trí xóa (xóa trong bảng nào để tiện hoàn tác)
    /// </summary>
    public string? DuongDan { get; set; }

    public DateTime? NgayXoa { get; set; }

    public int? MaNguoiDung { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiDungHoanTac { get; set; }

    public int? MaDonViSuDung { get; set; }

    public int? MaChiNhanh { get; set; }

    public byte? TrangThai { get; set; }
}
