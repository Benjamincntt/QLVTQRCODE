namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class CreatedSupplyRequest
{
    public string MaVatTu { get; set; } = string.Empty;
    
    public string TenVatTu { get; set; } = string.Empty;
    
    public string DonViTinh { get; set; } = string.Empty;
    
    // public string NguoiTao { get; set; } = string.Empty;
    //
    // public int NguoiTaoId { get; set; }
    public string GhiChu { get; set; } = string.Empty;
    
    public string MoTa { get; set; } = string.Empty;
}