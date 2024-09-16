namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class SupplyTicketDetailRequest
{
    public int IdVatTu { get; set; }

    public string TenVatTu { get; set; } = string.Empty;

    public string DonViTinh { get; set; } = string.Empty;

    public bool IsSystemSupply { get; set; }
    public int SoLuong { get; set; } = 0;

    public string ThongSoKyThuat { get; set; } = string.Empty;

    public string GhiChu { get; set; } = string.Empty;
}