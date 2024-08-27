namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuNguoiDungModunLoaiKieuNguoiDung
{
    public int MaKieuNguoiDung { get; set; }

    public int MaModun { get; set; }

    public int MaLoaiKieuNguoiDung { get; set; }

    /// <summary>
    /// 0
    /// </summary>
    public byte? ViTri { get; set; }

    public virtual TbKieuNguoiDung MaKieuNguoiDungNavigation { get; set; } = null!;

    public virtual TbLoaiKieuNguoiDung MaLoaiKieuNguoiDungNavigation { get; set; } = null!;

    public virtual TbModun MaModunNavigation { get; set; } = null!;
}
