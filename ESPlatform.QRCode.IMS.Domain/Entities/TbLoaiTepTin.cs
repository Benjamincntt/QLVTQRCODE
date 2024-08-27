namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLoaiTepTin
{
    public int MaLoaiTepTin { get; set; }

    public string TenLoaiTepTin { get; set; } = null!;

    public virtual ICollection<TbTepTin> TbTepTins { get; set; } = new List<TbTepTin>();
}
