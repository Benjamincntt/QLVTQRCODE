namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbViTriCongViec
{
    public int Id { get; set; }

    public string? TenViTriCongViec { get; set; }

    public int? MaCoCauToChuc { get; set; }

    public string? MoTaCongViec { get; set; }

    public string? YeuCauCongViec { get; set; }

    public DateTime? NgayThem { get; set; }

    public int? MaNguoiThem { get; set; }

    public DateTime? NgaySua { get; set; }

    public int? MaNguoiSua { get; set; }

    public byte? TrangThai { get; set; }

    public int? MaDonViSuDung { get; set; }

    /// <summary>
    /// 0: Không gửi đánh giá/ 1: Gửi đánh ra lên đơn vị cấp trên
    /// </summary>
    public byte? IsGuiDanhGia { get; set; }

    public string? MaDoiTuongKy { get; set; }
}
