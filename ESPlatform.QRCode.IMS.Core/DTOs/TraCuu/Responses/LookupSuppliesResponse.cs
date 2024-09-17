using System.ComponentModel;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;

namespace ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;

public class LookupSuppliesResponse
{
    public string MaVatTu { get; set; } = string.Empty;

    public string TenVatTu { get; set; } = string.Empty;
    [DisplayName("Mã kho")] 
    public string OrganizationCode { get; set; } = string.Empty;
    
    [DisplayName("Mã kho phụ")] 
    public string SubInventoryCode { get; set; } = string.Empty;

    public string LotNumber { get; set; } = string.Empty;

    public string DonViTinh { get; set; } = string.Empty;

    public string Image { get; set; } = string.Empty;

    public List<string> ImagePaths { get; set; } = new List<string>();

    public List<SuppliesLocation> SuppliesLocation { get; set; } = new List<SuppliesLocation>();
}