namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class TbAudience
{
    public string ClientId { get; set; } = null!;

    public string? Base64Secret { get; set; }

    public string? Name { get; set; }

    public DateTime? NgayTao { get; set; }
}
