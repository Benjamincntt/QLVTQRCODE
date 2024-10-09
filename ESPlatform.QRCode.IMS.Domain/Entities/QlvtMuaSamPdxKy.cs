namespace ESPlatform.QRCode.IMS.Domain.Entities;

public class QlvtMuaSamPdxKy
{
    public string? UsbSerial { get; set; }

    public int Id { get; set; }

    public int? PhieuDeXuatId { get; set; }

    public int? NguoiKyId { get; set; }

    public string? LyDo { get; set; }

    public short? ThuTuKy { get; set; }

    public byte? TrangThai { get; set; }

    public string? MaDoiTuongKy { get; set; }

    public string? ToaDo { get; set; }

    public DateTime? NgayKy { get; set; }

    public int? VanBanId { get; set; }

    public DateTime? NgayTao { get; set; }

    public int? NguoiTao { get; set; }
    
    public int? Page { get; set; }

    public double? PageHeight { get; set; }

    public virtual QlvtMuaSamPhieuDeXuat? PhieuDeXuat { get; set; }
}