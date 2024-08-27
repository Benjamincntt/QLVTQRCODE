namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuNguoiDung
{
    public int MaKieuNguoiDung { get; set; }

    public string TenKieuNguoiDung { get; set; } = null!;

    public bool LaQuanTriVienCaoCap { get; set; }

    public int MaDonViSuDung { get; set; }

    public bool MacDinh { get; set; }

    public int MaLoaiKieuNguoiDung { get; set; }

    public string? MoTa { get; set; }

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;

    public virtual TbLoaiKieuNguoiDung MaLoaiKieuNguoiDungNavigation { get; set; } = null!;

    public virtual ICollection<TbKieuNguoiDungChucNangLoaiKieuNguoiDung> TbKieuNguoiDungChucNangLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungChucNangLoaiKieuNguoiDung>();

    public virtual ICollection<TbKieuNguoiDungModunLoaiKieuNguoiDung> TbKieuNguoiDungModunLoaiKieuNguoiDungs { get; set; } = new List<TbKieuNguoiDungModunLoaiKieuNguoiDung>();

    public virtual ICollection<TbNguoiDung> TbNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
