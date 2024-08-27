namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbLsNguoiDungXoaModun
{
    public int MaLsNguoiDungXoaModun { get; set; }

    public int? MaNguoiDung { get; set; }

    public string? TenModun { get; set; }

    public DateTime? ThoiGian { get; set; }
}
