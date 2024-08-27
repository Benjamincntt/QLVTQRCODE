namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class NguoiDungViTriCongViec
{
    public int MaNguoiDungViTriCongViec { get; set; }

    public int? MaNguoiDung { get; set; }

    public int? MaViTriCongViec { get; set; }

    public DateTime? TuNgay { get; set; }

    public DateTime? DenNgay { get; set; }

    public bool? HienTai { get; set; }

    /// <summary>
    /// =1:(tồn tại);=10(đã xóa)
    /// </summary>
    public int? TrangThai { get; set; }

    public int? MaCoCauToChuc { get; set; }

    public bool? ViTriChinh { get; set; }
}
