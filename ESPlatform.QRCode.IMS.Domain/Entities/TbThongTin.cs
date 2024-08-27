namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbThongTin
{
    public int MaThongTin { get; set; }

    public int MaDonViSuDung { get; set; }

    public string TieuDe { get; set; } = null!;

    public string Anh { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;
}
