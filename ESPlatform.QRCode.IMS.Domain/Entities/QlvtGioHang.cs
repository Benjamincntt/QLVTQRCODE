namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtGioHang
{
    public int GioHangId { get; set; }

    public int? UserId { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public int? VatTuId { get; set; }

    public decimal? SoLuong { get; set; }

    public string? ThongSoKyThuat { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public bool? IsSystemSupply { get; set; }
}

