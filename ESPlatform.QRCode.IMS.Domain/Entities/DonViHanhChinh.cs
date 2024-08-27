namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class DonViHanhChinh
{
    public int Id { get; set; }

    public string? MaDiaChi { get; set; }

    public string? TenDiaChi { get; set; }

    public int? MaDonViSuDung { get; set; }

    public byte? TrangThai { get; set; }

    public string? NgayTao { get; set; }

    public string? NgaySua { get; set; }

    public int? MaNguoiTao { get; set; }

    public int? MaNguoiSua { get; set; }

    /// <summary>
    /// 0: quốc gia, 1: tinh, 2: quan, 3: phuong, 4: phố, 5: tổ
    /// </summary>
    public int? Cap { get; set; }

    public int? MaDiaChiCha { get; set; }
}
