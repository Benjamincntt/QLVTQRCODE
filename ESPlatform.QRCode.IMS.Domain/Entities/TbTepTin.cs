namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbTepTin
{
    public int MaTepTin { get; set; }

    public string TenTepTin { get; set; } = null!;

    public string DuongDan { get; set; } = null!;

    public string? DuongDanM3u8 { get; set; }

    public int MaLoaiTepTin { get; set; }

    public int? MaKhoaHoc { get; set; }

    public double? DungLuong { get; set; }

    public int? MaDonViSuDung { get; set; }

    public string? DuongDanFileConvert { get; set; }

    public string? DoPhanGiai { get; set; }

    public int? MaMayChuServerFile { get; set; }

    public string? DuongDanFileGoc { get; set; }

    public bool? DaXoa { get; set; }

    public virtual TbDonViSuDung? MaDonViSuDungNavigation { get; set; }

    public virtual TbLoaiTepTin MaLoaiTepTinNavigation { get; set; } = null!;

    public virtual TbMayChuServerFile? MaMayChuServerFileNavigation { get; set; }
}
