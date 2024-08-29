namespace ESPlatform.QRCode.IMS.Domain.Entities;

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
}
