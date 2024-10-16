using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class CreatedSupplyRequest
{
    [DisplayName("Tên vật tư")]
    public string TenVatTu { get; set; } = string.Empty;
    [DisplayName("Đơn vị tính")]
    public string DonViTinh { get; set; } = string.Empty;
    [DisplayName("Ghi chú")]
    public string GhiChu { get; set; } = string.Empty;
    [DisplayName("Thông số kỹ thuật")]
    public string ThongSoKyThuat { get; set; } = string.Empty;
    [DisplayName("Mã vật tư")]
    public string MaVatTu { get; set; } = string.Empty;
    [DisplayName("Xuất xứ")]
    public string XuatXu { get; set; } = string.Empty;
    
    public int DonGia { get; set; }
    public int SoLuong { get; set; }
}