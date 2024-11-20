using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESPlatform.QRCode.IMS.Domain.Entities;
[DisplayName("Vật tư mới")]
public partial class QlvtMuaSamVatTuNew
{
    public int VatTuNewId { get; set; }

    public string TenVatTu { get; set; } = null!;

    public string? DonViTinh { get; set; }

    public string? GhiChu { get; set; }

    public string? ThongSoKyThuat { get; set; }

    public string? Image { get; set; }

    public string? MaVatTu { get; set; }

    public string? XuatXu { get; set; }

    public decimal? DonGia { get; set; }
    
    public decimal? SoLuong { get; set; }
}
