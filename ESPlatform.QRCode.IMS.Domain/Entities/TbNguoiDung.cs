using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbNguoiDung
{
    public int MaNguoiDung { get; set; }

    public string? MaDinhDanh { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Ho { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string? AnhDaiDien { get; set; }

    public DateTime? NgaySinh { get; set; }

    public bool? GioiTinh { get; set; }

    public string? HocHam { get; set; }

    public string? HocVi { get; set; }

    public string? DiaChi { get; set; }

    public string? NoiCongTac { get; set; }

    public string? SoDienThoai { get; set; }

    public bool? KichHoat { get; set; }

    public string? TieuSu { get; set; }

    public double? TienTrongTaiKhoan { get; set; }

    public int? SoNguoiDungThich { get; set; }

    public bool? CoChoPhepHienThi { get; set; }

    public DateTime NgayTao { get; set; }

    public int MaTimeZone { get; set; }

    public int MaKieuNguoiDung { get; set; }

    public int MaDonViSuDung { get; set; }

    public bool LaQuanTriVienCaoCap { get; set; }

    public bool DaXoa { get; set; }

    public string? Salt { get; set; }

    public int? IdluongXuLy { get; set; }

    public int? IddonVi { get; set; }

    public DateTime? ThoiGianMatKhau { get; set; }

    public string? QueQuan { get; set; }

    public string? ChucVu { get; set; }

    /// <summary>
    /// Lưu giá trị theo bảng Tb_ViTriCongViec
    /// </summary>
    public int? ViTri { get; set; }

    public int? PhienBanSuDung { get; set; }

    public string? OtpSecret { get; set; }

    public string? Info { get; set; }

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;

    public virtual TbKieuNguoiDung MaKieuNguoiDungNavigation { get; set; } = null!;

    public virtual TbTimeZone MaTimeZoneNavigation { get; set; } = null!;

    public virtual ICollection<TbHopThuDen> TbHopThuDens { get; set; } = new List<TbHopThuDen>();

    public virtual ICollection<TbLsNguoiDungCapNhatNguoiDung> TbLsNguoiDungCapNhatNguoiDungMaNguoiBiTacDongNavigations { get; set; } = new List<TbLsNguoiDungCapNhatNguoiDung>();

    public virtual ICollection<TbLsNguoiDungCapNhatNguoiDung> TbLsNguoiDungCapNhatNguoiDungMaNguoiTacDongNavigations { get; set; } = new List<TbLsNguoiDungCapNhatNguoiDung>();

    public virtual ICollection<TbLsNguoiDungDonViSuDung> TbLsNguoiDungDonViSuDungs { get; set; } = new List<TbLsNguoiDungDonViSuDung>();

    public virtual ICollection<TbLsNguoiDungTaoNguoiDung> TbLsNguoiDungTaoNguoiDungMaNguoiBiTacDongNavigations { get; set; } = new List<TbLsNguoiDungTaoNguoiDung>();

    public virtual ICollection<TbLsNguoiDungTaoNguoiDung> TbLsNguoiDungTaoNguoiDungMaNguoiTacDongNavigations { get; set; } = new List<TbLsNguoiDungTaoNguoiDung>();

    public virtual ICollection<TbLsNguoiDungXoaNguoiDung> TbLsNguoiDungXoaNguoiDungMaNguoiBiTacDongNavigations { get; set; } = new List<TbLsNguoiDungXoaNguoiDung>();

    public virtual ICollection<TbLsNguoiDungXoaNguoiDung> TbLsNguoiDungXoaNguoiDungMaNguoiTacDongNavigations { get; set; } = new List<TbLsNguoiDungXoaNguoiDung>();

    public virtual ICollection<TbNguoiDungCoCauToChucO> TbNguoiDungCoCauToChucOs { get; set; } = new List<TbNguoiDungCoCauToChucO>();

    public virtual ICollection<TbTinNhan> TbTinNhans { get; set; } = new List<TbTinNhan>();

    public virtual ICollection<TbChucNang> MaChucNangs { get; set; } = new List<TbChucNang>();

    public virtual ICollection<TbModun> MaModuns { get; set; } = new List<TbModun>();

    public virtual ICollection<TbTinNhan> MaTinNhans { get; set; } = new List<TbTinNhan>();
}
