using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Domain.Entities;
[DisplayName("Phiếu đề xuất")]
public partial class QlvtMuaSamPhieuDeXuat
{   
    [DisplayName("Id Phiếu đề xuất")]
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

    public string? GhiChu { get; set; }

    public virtual ICollection<QlvtMuaSamPdxKy> QlvtMuaSamPdxKies { get; set; } = new List<QlvtMuaSamPdxKy>();

    public virtual ICollection<QlvtPhieuTrangThai> QlvtPhieuTrangThais { get; set; } = new List<QlvtPhieuTrangThai>();

    public virtual ICollection<QlvtVanBanKy> QlvtVanBanKies { get; set; } = new List<QlvtVanBanKy>();
}
