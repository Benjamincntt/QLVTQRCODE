namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbDonViSuDungModun
{
    public int MaDonViSuDung { get; set; }

    public int MaModun { get; set; }

    public bool? KichHoat { get; set; }

    public bool CoDungChung { get; set; }

    public virtual TbDonViSuDung MaDonViSuDungNavigation { get; set; } = null!;

    public virtual TbModun MaModunNavigation { get; set; } = null!;
}
