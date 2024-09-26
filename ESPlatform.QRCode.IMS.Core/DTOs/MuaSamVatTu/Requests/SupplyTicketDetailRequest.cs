using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class SupplyTicketDetailRequest
{
    [DisplayName("Id vật tư")]
    public int VatTuId { get; set; }
    [DisplayName("Tên vật tư")]
    public string TenVatTu { get; set; } = string.Empty;
    [DisplayName("Đơn vị tính")]
    public string DonViTinh { get; set; } = string.Empty;
    public bool IsSystemSupply { get; set; }
    [DisplayName("Số lượng")]
    public int SoLuong { get; set; } = 0;
    [DisplayName("Thông số kỹ thuật")]
    public string ThongSoKyThuat { get; set; } = string.Empty;
    [DisplayName("Ghi chú")]
    public string GhiChu { get; set; } = string.Empty;
}