namespace ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;

public class CartSupplyResponse
{
    public int VatTuId { get; set; }
    public bool IsSystemSupply { get; set; }
    public string Image { get; set; } = string.Empty;
    public string TenVatTu { get; set; } = string.Empty;
    public decimal SoLuong { get; set; } = 0;
    public string ThongSoKyThuat { get; set; } = string.Empty;
    public string GhiChu { get; set; } = string.Empty;
    public int GioHangId { get; set; }
    public decimal DonGia { get; set; } = 0;
}