namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbMayChuServerFile
{
    public int MaMayChuServerFile { get; set; }

    public string? DiaChiMayChuServerFile { get; set; }

    public bool? MacDinh { get; set; }

    public virtual ICollection<TbTepTin> TbTepTins { get; set; } = new List<TbTepTin>();
}
