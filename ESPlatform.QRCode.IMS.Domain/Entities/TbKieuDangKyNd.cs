namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuDangKyNd
{
    public int MaKieuDangKyNd { get; set; }

    public string TenKieuDangKyNd { get; set; } = null!;

    public string TenHienThi { get; set; } = null!;

    public virtual ICollection<TbDonViSuDung> TbDonViSuDungs { get; set; } = new List<TbDonViSuDung>();
}
