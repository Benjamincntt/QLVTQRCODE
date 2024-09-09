using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class SupplyListRequest : PagingFilter
{
    public string? TenVatTu { get; set; } = string.Empty;
    
    public string? MaVatTu { get; set; } = string.Empty;

    public int IdKho { get; set; } = 0;

    public int IdViTri { get; set; } = 0;
}