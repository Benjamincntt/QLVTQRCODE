﻿using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyOrderDetailResponse
{
    public List<string> ImagePaths { get; set; } = new List<string>();
    
    public string TenVatTu { get; set; } = string.Empty;
    
    public string ThongSoKyThuat { get; set; } = string.Empty;
    
    public string GhiChu { get; set; } = string.Empty;
    
    public decimal DonGia { get; set; }
    
    public decimal OnhandQuantity { get; set; } = 0;

    public Is007A Is007A { get; set; } = Is007A.TonKho;
}