﻿using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;

public class InventoryCheckResponse
{

    public int Tag { get; set; }
    
    public string MaVatTu { get; set; } = string.Empty;
    
    public string TenVatTu { get; set; } = string.Empty;
    

    public string ViTri { get; set; } = string.Empty;
    [DisplayName("Tên kỳ kiểm kê")]
    public string PhysicalInventoryName { get; set; } = string.Empty;
    [DisplayName("Mã kho")]
    public string OrganizationCode { get; set; } = string.Empty;

    public string TinhTrang { get; set; } = string.Empty;
    [DisplayName("Mã kho phụ")]
    public string SubInventoryCode { get; set; } = string.Empty;
    [DisplayName("Tên kho phụ")]
    public string SubInventoryName { get; set; } = string.Empty;

    public string LotNumber { get; set; } = string.Empty;
    
    public string DonViTinh { get; set; } = string.Empty;

    public int IdToMay { get; set; }
    
    public int IdGiaKe { get; set; }
    
    public int IdNgan { get; set; }
    
    public int IdHop { get; set; }
    
    public decimal SoLuongSoSach { get; set; }

    public decimal SoLuongKiemKe { get; set; }

    public decimal SoLuongChenhLech { get; set; }
    
    public string Image { get; set; } = string.Empty;

    public List<string> ImagePaths { get; set; } = new List<string>();
}
