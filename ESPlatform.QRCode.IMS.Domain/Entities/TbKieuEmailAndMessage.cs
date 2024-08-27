namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuEmailAndMessage
{
    public int MaKieuEmailAndMessage { get; set; }

    public string TenKieuEmailAndMessage { get; set; } = null!;

    public string? DuongDanVi { get; set; }

    public string? DuongDanEn { get; set; }

    public string? NoiDungTempleteVi { get; set; }

    public string? NoiDungTempleteEn { get; set; }

    public byte? MaLoaiEmailAndMessage { get; set; }
}
