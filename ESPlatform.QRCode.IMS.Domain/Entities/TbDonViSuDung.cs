namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbDonViSuDung
{
    public int MaDonViSuDung { get; set; }

    public string? TenDonViSuDung { get; set; }

    public string? BiDanh { get; set; }

    public string? DiaChi { get; set; }

    public string? TenMienRieng { get; set; }

    public string TenMienCon { get; set; } = null!;

    /// <summary>
    /// 0: sở; 1: tiểu học; 2: thcs; 3: thpt; 4: mầm non; 5: phòng
    /// </summary>
    public int? CapHoc { get; set; }

    public int? ParentId { get; set; }

    public bool? KichHoat { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool CoDungThu { get; set; }

    public string Email { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? Logo { get; set; }

    public string? Favicon { get; set; }

    public string? Banner { get; set; }

    public string? TieuDeWeb { get; set; }

    public bool? TrangChuNdlaTrangDangNhap { get; set; }

    public string? LoiThongCaoBenNgoai { get; set; }

    public string? LoiThongCaoBenTrong { get; set; }

    public string LogoKhoaHoc { get; set; } = null!;

    public int MaNgonNgu { get; set; }

    public int MaTimeZone { get; set; }

    public int MaKieuDangKyNd { get; set; }

    public int? MaTemplate { get; set; }

    public bool? ChoPhepNhieuTaiKhoanDangNhap { get; set; }

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public int SoLuongToiDaNguoiTruyCap { get; set; }

    public double DungLuongToiDa { get; set; }

    public int SoLuongKhoaHocToiDa { get; set; }

    public string? ThongBao { get; set; }

    public DateTime? NgayKichHoat { get; set; }

    public int? SoLuongNgayDungToiDa { get; set; }

    public string? NoiDungBanner { get; set; }

    public bool? CoSuDungTrangChu { get; set; }

    /// <summary>
    /// 1: kiểu mặc định; 2: kiểu mặc định đủ 6 ký tự trở lên; 3: kiểu gồm cả chữ và số; 4: kiểu gồm cả chữ và số đủ 6 ký tự trở lên
    /// </summary>
    public byte? KieuMatKhau { get; set; }

    public int? ThoiGianMatKhau { get; set; }

    public int? SoLanDangNhapSai { get; set; }

    public bool? LaDonViTinh { get; set; }

    public double? GiaXem { get; set; }

    /// <summary>
    /// Cài đặt tự động đồng bộ; =1 là tự động
    /// </summary>
    public bool? SynchronizeAuto { get; set; }

    /// <summary>
    /// Thời gian đồng bộ trong ngày, theo giờ (từ 1-24)
    /// </summary>
    public int? SynchronizeTime { get; set; }

    public virtual TbKieuDangKyNd MaKieuDangKyNdNavigation { get; set; } = null!;

    public virtual TbNgonNgu MaNgonNguNavigation { get; set; } = null!;

    public virtual TbTemplate? MaTemplateNavigation { get; set; }

    public virtual TbTimeZone MaTimeZoneNavigation { get; set; } = null!;

    public virtual ICollection<TbDonViSuDungModun> TbDonViSuDungModuns { get; set; } = new List<TbDonViSuDungModun>();

    public virtual ICollection<TbHinhNenVanBang> TbHinhNenVanBangs { get; set; } = new List<TbHinhNenVanBang>();

    public virtual ICollection<TbKieuNguoiDung> TbKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDung>();

    public virtual ICollection<TbLsNguoiDungDonViSuDung> TbLsNguoiDungDonViSuDungs { get; set; } = new List<TbLsNguoiDungDonViSuDung>();

    public virtual ICollection<TbNguoiDung> TbNguoiDungs { get; set; } = new List<TbNguoiDung>();

    public virtual ICollection<TbTepTin> TbTepTins { get; set; } = new List<TbTepTin>();

    public virtual ICollection<TbTheme> TbThemes { get; set; } = new List<TbTheme>();

    public virtual ICollection<TbThongTin> TbThongTins { get; set; } = new List<TbThongTin>();

    public virtual ICollection<TbTemplate> MaTemplates { get; set; } = new List<TbTemplate>();
}
