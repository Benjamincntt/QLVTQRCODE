namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbModun
{
    public int MaModun { get; set; }

    public string TenHienThi { get; set; } = null!;

    public string TenModun { get; set; } = null!;

    public string TenController { get; set; } = null!;

    public string TenAction { get; set; } = null!;

    public bool MacDinh { get; set; }

    public bool? CoDungChung { get; set; }

    public string? GioiThieu { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? OchiNhanh { get; set; }

    public int? MaNhomModun { get; set; }

    public string? PhienBan { get; set; }

    public bool? KichHoat { get; set; }

    public string? Icon { get; set; }

    public string? ThayTheModule { get; set; }

    public int? SoRequestToiDa { get; set; }

    public bool? DaXoa { get; set; }

    public int? SoThuTu { get; set; }

    public string? TenControllerTuongDuong { get; set; }

    public virtual TbNhomModun? MaNhomModunNavigation { get; set; }

    public virtual ICollection<TbChucNang> TbChucNangs { get; set; } = new List<TbChucNang>();

    public virtual ICollection<TbDonViSuDungModun> TbDonViSuDungModuns { get; set; } = new List<TbDonViSuDungModun>();

    public virtual ICollection<TbKieuNguoiDungModunLoaiKieuNguoiDung> TbKieuNguoiDungModunLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungModunLoaiKieuNguoiDung>();

    public virtual ICollection<TbModunChaCon> TbModunChaConMaModunChaNavigations { get; set; } = new List<TbModunChaCon>();

    public virtual ICollection<TbModunChaCon> TbModunChaConMaModunConNavigations { get; set; } = new List<TbModunChaCon>();

    public virtual ICollection<TbLoaiKieuNguoiDung> MaLoaiKieuNguoiDungs { get; set; } = new List<TbLoaiKieuNguoiDung>();

    public virtual ICollection<TbNguoiDung> MaNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
