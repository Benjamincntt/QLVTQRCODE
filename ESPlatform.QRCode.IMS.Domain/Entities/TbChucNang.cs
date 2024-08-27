namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbChucNang
{
    public int MaChucNang { get; set; }

    public string TenHienThi { get; set; } = null!;

    public string TenChucNang { get; set; } = null!;

    public string? GioiThieu { get; set; }

    public int MaMoDun { get; set; }

    public bool? KichHoat { get; set; }

    public virtual TbModun MaMoDunNavigation { get; set; } = null!;

    public virtual ICollection<TbKieuNguoiDungChucNangLoaiKieuNguoiDung> TbKieuNguoiDungChucNangLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungChucNangLoaiKieuNguoiDung>();

    public virtual ICollection<TbLoaiKieuNguoiDung> MaLoaiKieuNguoiDungs { get; set; } = new List<TbLoaiKieuNguoiDung>();

    public virtual ICollection<TbNguoiDung> MaNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
