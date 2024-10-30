namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;

public class SupplyListResponseItem
{
    public int VatTuId { get; set; }
    
    public string TenVatTu { get; set; } = string.Empty;
    
    public string MaVatTu { get; set; } = string.Empty;
    
    public string XuatXu { get; set; } = string.Empty;
    
    public string DonViTinh { get; set; } = string.Empty;
    
    public bool IsSystemSupply { get; set; }
    
    public string Image { get; set; } = string.Empty;

    public decimal DonGia { get; set; } = 0; 
    
    public string ThongSoKyThuat { get; set; } = string.Empty;
    
}