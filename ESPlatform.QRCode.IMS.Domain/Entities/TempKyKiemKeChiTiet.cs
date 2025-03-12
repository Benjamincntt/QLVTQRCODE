namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TempKyKiemKeChiTiet
{
    public int IdKy { get; set; }

    public int IdVatTu { get; set; }

    public int? IdKho { get; set; }

    public decimal? SlSoSach { get; set; }

    public decimal? SlChenhLech { get; set; }

    public string SoThe { get; set; } = null!;

    public short TrangThai { get; set; }

    public DateTime? NgayKiemKe { get; set; }

    public decimal? PtMatPhamChat { get; set; }

    public decimal? PtKemPhamChat { get; set; }

    public decimal? SluDong { get; set; }

    public decimal? SlThanhLy { get; set; }

    public decimal? PtuDong { get; set; }

    public decimal? TongSoMpcKpc { get; set; }

    public decimal? SlMatPhamChat { get; set; }

    public decimal? SlKemPhamChat { get; set; }

    public int? IdThayDoi { get; set; }

    public int? IdThe { get; set; }
}
