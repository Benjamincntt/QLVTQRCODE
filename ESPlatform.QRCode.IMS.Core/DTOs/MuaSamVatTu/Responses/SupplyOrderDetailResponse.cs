namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyOrderDetailResponse
{
    public List<string> ImagePaths { get; set; } = new List<string>();
    
    public string TenVatTu { get; set; } = string.Empty;
    
    public string ThongSoKyThuat { get; set; } = string.Empty;
    
    public string GhiChu { get; set; } = string.Empty;
    
    public int DonGia { get; set; }
}