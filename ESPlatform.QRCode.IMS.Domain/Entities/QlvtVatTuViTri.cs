namespace ESPlatform.QRCode.IMS.Domain.Entities;

public partial class QlvtVatTuViTri
{
    public int IdvatTu { get; set; }

    public string MaVatTu { get; set; } = null!;

    public string TenVatTu { get; set; } = null!;

    public int? IdkhoErp { get; set; }

    public int? IdtoMay { get; set; }

    public int? IdgiaKe { get; set; }

    public int? Idngan { get; set; }

    public int? Idhop { get; set; }
}
