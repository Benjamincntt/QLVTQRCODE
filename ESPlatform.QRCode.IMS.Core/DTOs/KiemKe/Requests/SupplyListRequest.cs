using System.ComponentModel;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class SupplyListRequest : PagingFilter
{   
    [DisplayName("Tên vật tư")]
    public string? TenVatTu { get; set; } 
    [DisplayName("Mã vật tư")]
    public string? MaVatTu { get; set; }

    [DisplayName("Id kho")] 
    public int IdKho { get; set; } = 0;
    [DisplayName("Id nhóm 8")]
    public List<int>? ListIdToMay { get; set; } 
    [DisplayName("Id nhóm 9")]
    public List<int>? ListIdGiaKe { get; set; } 
    [DisplayName("Id nhóm 10")]
    public List<int>? ListIdNgan { get; set; } 
    public List<string>? ListMaNhom { get; set; } 
    // Mặc định lấy các vật tư trong hệ thống
    public bool? IsSystemSupply { get; set; } = true;
    // True: thuộc bảng tồn kho, false: thuộc bảng vật tư/vật tư mới
    public bool? Is007A { get; set; } = false;
}