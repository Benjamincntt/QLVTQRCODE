namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbItem
{
    public int MaItem { get; set; }

    public string TenItem { get; set; } = null!;

    public string Mau { get; set; } = null!;

    public int MaTheme { get; set; }

    public int? MaItemMacDinh { get; set; }

    public virtual TbItemMacDinh? MaItemMacDinhNavigation { get; set; }

    public virtual TbTheme MaThemeNavigation { get; set; } = null!;
}
