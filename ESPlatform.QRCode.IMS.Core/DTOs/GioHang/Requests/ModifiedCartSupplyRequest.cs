﻿namespace ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;

public class ModifiedCartSupplyRequest
{
    public bool IsSystemSupply { get; set; }
    
    public string? ThongSoKyThuat { get; set; }
    
    public string? GhiChu { get; set; }
}