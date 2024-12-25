namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TempVatTuTonKho
{
    public int VatTuId { get; set; }

    public int? KhoId { get; set; }

    public string? MaKho { get; set; }

    public string? TenVatTu { get; set; }

    public string? MaVatTu { get; set; }

    public string? DonViTinh { get; set; }

    public decimal? SoLuongTon { get; set; }

    public string? SoLoVatTu { get; set; }

    public string? MaKhoPhu { get; set; }
}
