namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbNhomModun
{
    public int MaNhomModun { get; set; }

    public string TenNhomModun { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<TbModun> TbModuns { get; set; } = new List<TbModun>();
}
