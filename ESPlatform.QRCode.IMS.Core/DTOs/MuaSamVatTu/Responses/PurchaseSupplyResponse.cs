namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class PurchaseSupplyResponse
{
    public List<string> ImagePaths { get; set; } = new List<string>();
    
    public string TenVatTu { get; set; } = string.Empty;
    
    public string MoTa { get; set; } = string.Empty;
}