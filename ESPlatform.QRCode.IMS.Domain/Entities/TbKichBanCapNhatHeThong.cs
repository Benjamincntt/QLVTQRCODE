namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKichBanCapNhatHeThong
{
    public int MaKichBanCapNhatHeThong { get; set; }

    public int? MaModun { get; set; }

    public int? MaTemplate { get; set; }

    public string? DuongDanCopy { get; set; }

    public string? DuongDanPaste { get; set; }

    public string? LoaiFile { get; set; }

    public DateTime? NgayTao { get; set; }
}
