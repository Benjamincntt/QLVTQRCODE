namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbTemplate
{
    public int MaTemplate { get; set; }

    public string? TenHienThi { get; set; }

    public string TenTemplate { get; set; } = null!;

    public bool MacDinh { get; set; }

    public string? AnhLayout { get; set; }

    public string DuongDan { get; set; } = null!;

    public bool RiengTu { get; set; }

    public bool DaDung { get; set; }

    public string? PhienBan { get; set; }

    public virtual ICollection<TbDonViSuDung> TbDonViSuDungs { get; set; } = new List<TbDonViSuDung>();

    public virtual ICollection<TbThemeMacDinh> TbThemeMacDinhs { get; set; } = new List<TbThemeMacDinh>();

    public virtual ICollection<TbTheme> TbThemes { get; set; } = new List<TbTheme>();

    public virtual ICollection<TbDonViSuDung> MaDonViSuDungs { get; set; } = new List<TbDonViSuDung>();
}
