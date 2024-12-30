namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtGioHang
{
    public int GioHangId { get; set; }

    public int? UserId { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public int? VatTuId { get; set; }

    public decimal? SoLuong { get; set; }

    /// <summary>
    /// Thông số kỹ thuật
    /// </summary>
    public string? ThongSoKyThuat { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? ThoiGianCapNhat { get; set; }

    public bool? IsSystemSupply { get; set; }

    /// <summary>
    /// vật tư 007A là vật tư được thêm vào từ bảng QLVT_VatTu  không tồn tại trong bảng vật tư tồn kho: QLVT_VatTu_TonKho 
    /// </summary>
    public short? Is007a { get; set; }
    
}

