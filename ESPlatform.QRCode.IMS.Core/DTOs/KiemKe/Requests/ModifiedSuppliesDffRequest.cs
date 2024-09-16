using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class ModifiedSuppliesDffRequest
{   [DisplayName("Số lượng mất phẩm chất")]
    public decimal? SoLuongMatPhamChat { get; set; } = 0;
    [DisplayName("Số lượng kém phẩm chất")]
    public decimal? SoLuongKemPhamChat { get; set; } = 0;
    [DisplayName("Số lượng ứ đọng")]
    public decimal? SoLuongDong { get; set; } = 0;
    [DisplayName("Số lượng đề nghị thanh lý")]
    public decimal? SoLuongDeNghiThanhLy { get; set; } = 0;
}