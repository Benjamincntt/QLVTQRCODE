﻿using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Domain.Entities;

[DisplayName("Vị trí của vật tư")]
public partial class QlvtVatTuViTri
{
    
    public int IdViTri { get; set; }
    
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
