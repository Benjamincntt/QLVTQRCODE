namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVatTuViTri
{
    public int IdVatTu { get; set; }

    public string MaVatTu { get; set; } = null!;

    public string TenVatTu { get; set; } = null!;

    public int? IdKhoErp { get; set; }

    public int? IdToMay { get; set; }

    public int? IdGiaKe { get; set; }

    public int? IdNgan { get; set; }

    public int? IdHop { get; set; }
    
    public string ViTri { get; set; } = string.Empty;
}
