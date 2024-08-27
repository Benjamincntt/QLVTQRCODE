namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbTimeZone
{
    public int MaTimeZone { get; set; }

    public string TenTimeZone { get; set; } = null!;

    public int MuiGio { get; set; }

    public virtual ICollection<TbDonViSuDung> TbDonViSuDungs { get; set; } = new List<TbDonViSuDung>();

    public virtual ICollection<TbNguoiDung> TbNguoiDungs { get; set; } = new List<TbNguoiDung>();
}
