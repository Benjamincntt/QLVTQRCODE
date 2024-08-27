namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuNguoiDungChucNangLoaiKieuNguoiDung
{
    public int MaKieuNguoiDung { get; set; }

    public int MaChucNang { get; set; }

    public int MaLoaiKieuNguoiDung { get; set; }

    public virtual TbChucNang MaChucNangNavigation { get; set; } = null!;

    public virtual TbKieuNguoiDung MaKieuNguoiDungNavigation { get; set; } = null!;

    public virtual TbLoaiKieuNguoiDung MaLoaiKieuNguoiDungNavigation { get; set; } = null!;
}
