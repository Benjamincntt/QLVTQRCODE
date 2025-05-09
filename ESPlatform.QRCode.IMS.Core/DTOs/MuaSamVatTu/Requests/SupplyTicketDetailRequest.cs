﻿using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class SupplyTicketDetailRequest
{
    [DisplayName("Giỏ Hàng Id")]
    public int GioHangId { get; set; }
     [DisplayName("Id vật tư")]
    public int VatTuId { get; set; }

    public bool IsSystemSupply { get; set; }
    [DisplayName("Số lượng")]
    public decimal SoLuong { get; set; } = 0;
    [DisplayName("Thông số kỹ thuật")]
    public string ThongSoKyThuat { get; set; } = string.Empty;
    [DisplayName("Ghi chú")]
    public string GhiChu { get; set; } = string.Empty;
    
}