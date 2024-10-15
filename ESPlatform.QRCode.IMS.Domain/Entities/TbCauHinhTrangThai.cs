namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbCauHinhTrangThai
{
    public int Id { get; set; }

    public string? TenTrangThai { get; set; }

    public string? MaMau { get; set; }

    public string? MoTa { get; set; }

    public int? GiaTri { get; set; }
}
