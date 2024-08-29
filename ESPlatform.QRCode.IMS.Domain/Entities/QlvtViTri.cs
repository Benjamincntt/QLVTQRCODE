namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtViTri
{
    public int Id { get; set; }

    public string? Ten { get; set; }

    public string? Ma { get; set; }

    public int? IdKhoErp { get; set; }

    public string? KichThuoc { get; set; }

    public string? MoTa { get; set; }

    public int? MaDonViSuDung { get; set; }

    public byte? TrangThai { get; set; }

    public DateTime? NgayThem { get; set; }

    public int? MaNguoiThem { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiSua { get; set; }

    /// <summary>
    /// =0 đây là mức cha
    /// &lt;&gt;0 đây là ID cha
    /// </summary>
    public int ParentId { get; set; }
}
