namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TempKyKiemKeChiTiet
{
    public int IdkyKiemKe { get; set; }

    public int IdvatTu { get; set; }

    public int? IdKho { get; set; }

    public decimal? SlsoSach { get; set; }

    public decimal? SlkiemKe { get; set; }

    public decimal? SlchenhLech { get; set; }

    public string SoThe { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public DateTime? NgayKiemKe { get; set; }

    public decimal? SlmatPhamChat { get; set; }

    public decimal? PhanTramKemPc { get; set; }

    public decimal? Sldong { get; set; }

    public decimal? SldeNghiThanhLy { get; set; }

    public decimal? PhanTramDong { get; set; }

    public decimal? TaiSanKemPc { get; set; }
}
