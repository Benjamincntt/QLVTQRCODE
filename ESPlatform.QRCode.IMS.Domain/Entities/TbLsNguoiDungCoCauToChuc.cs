namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLsNguoiDungCoCauToChuc
{
    public int MaLsNguoiDungCoCauToChucOes { get; set; }

    public int? MaNguoiDung { get; set; }

    public int? MaCoCauToChuc { get; set; }

    public string? ThaoTac { get; set; }

    public DateTime? NgayThaoTac { get; set; }
}
