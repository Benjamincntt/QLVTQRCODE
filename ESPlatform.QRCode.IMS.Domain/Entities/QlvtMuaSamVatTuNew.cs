namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtMuaSamVatTuNew
{
    public int VatTuNewId { get; set; }

    public string TenVatTu { get; set; } = null!;

    public string DonViTinh { get; set; }

    public string? GhiChu { get; set; }

    public string? MoTa { get; set; }
}
