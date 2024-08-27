namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbModunChaCon
{
    public int MaModunCha { get; set; }

    public int MaModunCon { get; set; }

    public int MaLoaiKieuNguoiDung { get; set; }

    public virtual TbLoaiKieuNguoiDung MaLoaiKieuNguoiDungNavigation { get; set; } = null!;

    public virtual TbModun MaModunChaNavigation { get; set; } = null!;

    public virtual TbModun MaModunConNavigation { get; set; } = null!;
}
