namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class ModifiedSuppliesLocationRequest
{
    public int IdToMay { get; set; }
    
    public int IdGiaKe { get; set; }
    
    public int IdNgan { get; set; }
    
    public int IdHop { get; set; }

    public string TenToMay { get; set; } = string.Empty;
    
    public string TenGiaKe { get; set; } = string.Empty;
    
    public string TenNgan { get; set; } = string.Empty;
    
    public string TenHop { get; set; } = string.Empty;
    
}