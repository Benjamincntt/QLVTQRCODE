namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbKieuEmailAndMessageDonViSuDung
{
    public int MaKieuEmailAndMessageDonViSuDung { get; set; }

    public string TenKieuEmailAndMessage { get; set; } = null!;

    public int MaDonViSuDung { get; set; }

    public string? Vi { get; set; }

    public string? En { get; set; }

    public byte? MaLoaiEmailAndMessage { get; set; }
}
