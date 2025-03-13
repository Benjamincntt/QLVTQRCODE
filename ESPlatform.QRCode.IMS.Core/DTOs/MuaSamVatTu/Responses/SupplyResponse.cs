namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyResponse : SupplyTicketDto
{
    public int VatTuId { get; set; } 
    
    public bool IsSystemSupply { get; set; }
    
    public string Image { get; set; } = string.Empty;
    
    public decimal DonGia { get; set; } 
    
}