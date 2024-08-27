namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbDieuHuongModule
{
    public int MaDieuHuongModule { get; set; }

    public int? MaDonViSuDung { get; set; }

    public string? TenController { get; set; }

    public string? TenControllerThayThe { get; set; }

    public int? MaModule { get; set; }

    public int? MaModuleThayThe { get; set; }

    public bool? TuDong { get; set; }
}
