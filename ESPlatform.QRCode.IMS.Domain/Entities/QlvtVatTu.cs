using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Domain.Entities;
[DisplayName("Vật tư")]
public partial class QlvtVatTu
{
    public int KhoId { get; set; }

    public string? MaVatTu { get; set; }

    public string? TenVatTu { get; set; }

    public string? DonViTinh { get; set; }

    public string? NguoiTao { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? NguoiTaoId { get; set; }

    public string? GhiChu { get; set; }

    public int VatTuId { get; set; }

    public short? TrangThaiInQr { get; set; }

    /// <summary>
    /// Ảnh đại diện, mặc định ảnh đầu tiên sẽ là ảnh mặc định hiển thị ban đầu. Khi tồn tại ảnh này thì cần check folder tương ứng xem còn ảnh ko
    /// </summary>
    public string? Image { get; set; }

    /// <summary>
    /// Thông số kỹ thuật
    /// </summary>
    public string? MoTa { get; set; }
}
