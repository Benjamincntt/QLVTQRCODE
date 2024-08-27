namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLoaiKieuNguoiDung
{
    public int MaLoaiKieuNguoiDung { get; set; }

    public string TenLoaiKieuNguoiDung { get; set; } = null!;

    public string TenNhanDang { get; set; } = null!;

    public virtual ICollection<TbKieuNguoiDungChucNangLoaiKieuNguoiDung> TbKieuNguoiDungChucNangLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungChucNangLoaiKieuNguoiDung>();

    public virtual ICollection<TbKieuNguoiDungModunLoaiKieuNguoiDung> TbKieuNguoiDungModunLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungModunLoaiKieuNguoiDung>();

    public virtual ICollection<TbKieuNguoiDung> TbKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDung>();

    public virtual ICollection<TbModunChaCon> TbModunChaCons { get; set; } = new List<TbModunChaCon>();

    public virtual ICollection<TbChucNang> MaChucNangs { get; set; } = new List<TbChucNang>();

    public virtual ICollection<TbModun> MaModuns { get; set; } = new List<TbModun>();
}
