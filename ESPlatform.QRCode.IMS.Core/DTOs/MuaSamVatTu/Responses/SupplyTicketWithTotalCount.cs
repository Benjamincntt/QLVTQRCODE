namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyTicketWithTotalCount
{
    // public int VatTuId { get; set; }
    public string TenVatTu { get; set; } = string.Empty;
    public string TenPhieu { get; set; } = string.Empty;
    public decimal SoLuong { get; set; }
}