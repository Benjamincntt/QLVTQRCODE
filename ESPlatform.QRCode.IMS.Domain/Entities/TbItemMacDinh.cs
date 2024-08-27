namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbItemMacDinh
{
    public int MaItemMacDinh { get; set; }

    public string TenItemMacDinh { get; set; } = null!;

    public string Mau { get; set; } = null!;

    public int MaThemeMacDinh { get; set; }

    public virtual TbThemeMacDinh MaThemeMacDinhNavigation { get; set; } = null!;

    public virtual ICollection<TbItem> TbItems { get; set; } = new List<TbItem>();
}
