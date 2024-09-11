namespace ESPlatform.QRCode.IMS.Core.DTOs.LapPhieu.Requests;

public class CreatedPurchaseOrderRequest
{
    public string MaPhieu { get; set; } = string.Empty;

    public string TenPhieu { get; set; } = string.Empty;

    public string MoTa { get; set; } = string.Empty;

    public int TrangThai { get; set; }
}