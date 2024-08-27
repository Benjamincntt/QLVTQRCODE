namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbHinhNenVanBang
{
    public int MaHinhNenVanBang { get; set; }

    public string HinhNen { get; set; } = null!;

    public int? MaDonViSuDung { get; set; }

    public string? Template { get; set; }

    public virtual TbDonViSuDung? MaDonViSuDungNavigation { get; set; }
}
