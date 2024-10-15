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
}