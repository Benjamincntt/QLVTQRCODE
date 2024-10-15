﻿using System.ComponentModel;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;

public class SupplyListRequest : PagingFilter
{   
    [DisplayName("Tên vật tư")]
    public string? TenVatTu { get; set; } = string.Empty;
    [DisplayName("Mã vật tư")]
    public string? MaVatTu { get; set; } = string.Empty;
    [DisplayName("Id kho")]
    public int IdKho { get; set; } = 0;
    [DisplayName("Id nhóm 8")]
    public int IdToMay { get; set; } = 0;
    [DisplayName("Id nhóm 9")]
    public int IdGiaKe { get; set; } = 0;
    [DisplayName("Id nhóm 10")]
    public int IdNgan { get; set; } = 0;
    public string? MaNhom { get; set; } = string.Empty;
}