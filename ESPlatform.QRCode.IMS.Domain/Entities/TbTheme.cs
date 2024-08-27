namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbTheme
{
    public int MaTheme { get; set; }

    public string? TenTheme { get; set; }

    public bool DuocChon { get; set; }

    public int MaDonViSuDung { get; set; }

    public bool DuocXoa { get; set; }

    public bool MacDinh { get; set; }

    public int? MaTemplate { get; set; }

    public int? MaThemeMacDinh { get; set; }

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;

    public virtual TbTemplate? MaTemplateNavigation { get; set; }

    public virtual TbThemeMacDinh? MaThemeMacDinhNavigation { get; set; }

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
