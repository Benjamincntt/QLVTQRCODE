namespace ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;

public class CreatedCartSupplyRequest
{
    public decimal SoLuong { get; set; } = 0;
    public string ThongSoKyThuat { get; set; } = string.Empty;
    public string GhiChu { get; set; } = string.Empty;
    public bool IsSystemSupply { get; set; } = true;
}