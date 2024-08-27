namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbThemeMacDinh
{
    public int MaThemeMacDinh { get; set; }

    public string TenThemeMacDinh { get; set; } = null!;

    public bool MacDinh { get; set; }

    public int MaTemplate { get; set; }

    public virtual TbTemplate MaTemplateNavigation { get; set; } = null!;

    public virtual ICollection<TbItemMacDinh> TbItemMacDinhs { get; set; } = new List<TbItemMacDinh>();

    public virtual ICollection<TbTheme> TbThemes { get; set; } = new List<TbTheme>();
}
