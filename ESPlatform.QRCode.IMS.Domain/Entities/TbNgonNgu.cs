namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbNgonNgu
{
    public int MaNgonNgu { get; set; }

    public string? TenNgonNgu { get; set; }

    public string? NhanDien { get; set; }

    public virtual ICollection<TbDonViSuDung> TbDonViSuDungs { get; set; } = new List<TbDonViSuDung>();
}
