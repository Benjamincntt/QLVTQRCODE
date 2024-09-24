namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtKyKiemKeChiTietDffBackup
{
    public int ChiTietDffId { get; set; }

    public int? KyKiemKeIdGoc { get; set; }

    public int? VatTuId { get; set; }

    public decimal? SoLuongMatPhamChat { get; set; }

    public decimal? PhanTramMatPhamChat { get; set; }

    public decimal? SoLuongKemPhamChat { get; set; }

    public decimal? PhanTramKemPhamChat { get; set; }

    public decimal? SoLuongDong { get; set; }

    public decimal? SoLuongDeNghiThanhLy { get; set; }

    public decimal? PhanTramDong { get; set; }

    public decimal? TsKemPcMatPc { get; set; }

    public int? KyKiemKeChiTietBackupId { get; set; }

    public virtual QlvtKyKiemKeChiTietBackup? KyKiemKeChiTietBackup { get; set; }
}

