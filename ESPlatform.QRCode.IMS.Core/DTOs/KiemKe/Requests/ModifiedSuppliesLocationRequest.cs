using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class ModifiedSuppliesLocationRequest
{
    [DisplayName("Id tổ máy")]
    public int? IdToMay { get; set; }
    [DisplayName("Id giá kệ")]
    public int? IdGiaKe { get; set; }
    [DisplayName("Id ngăn")]
    public int? IdNgan { get; set; }
    [DisplayName("Id hộc")]
    public int? IdHop { get; set; }
    [DisplayName("Tên tổ máy")]
    public string? TenToMay { get; set; } = string.Empty;
    [DisplayName("Tên giá kệ")]
    public string? TenGiaKe { get; set; } = string.Empty;
    [DisplayName("Tên ngăn")]
    public string? TenNgan { get; set; } = string.Empty;
    [DisplayName("Tên hộc")]
    public string? TenHop { get; set; } = string.Empty;
    
}