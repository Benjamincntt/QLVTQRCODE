namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKeChiTietDff
{
    public int Id { get; set; }

    public int? KyKiemKeChiTietId { get; set; }

    public int? VatTuId { get; set; }

    public decimal? SoLuongMatPhamChat { get; set; }

    public decimal? PhanTramMatPhamChat { get; set; }

    public decimal? SoLuongKemPhamChat { get; set; }

    public decimal? PhanTramKemPhamChat { get; set; }

    public decimal? SoLuongDong { get; set; }

    public decimal? SoLuongDeNghiThanhLy { get; set; }

    public decimal? PhanTramDong { get; set; }

    public decimal? TsKemPcMatPc { get; set; }
}
