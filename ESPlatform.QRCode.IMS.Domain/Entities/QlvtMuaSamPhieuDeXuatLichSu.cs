namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtMuaSamPhieuDeXuatLichSu
{
    public int Id { get; set; }

    public int? IdphieuDeXuat { get; set; }

    public string? NguoiThucHien { get; set; }

    public DateTime? NgayThucHien { get; set; }

    public string? LyDo { get; set; }

    /// <summary>
    /// =0: Nháp; =1: tạo và gửi duyệt; =2: duyệt; =4 Hủy duyệt
    /// </summary>
    public byte? TrangThai { get; set; }
}
