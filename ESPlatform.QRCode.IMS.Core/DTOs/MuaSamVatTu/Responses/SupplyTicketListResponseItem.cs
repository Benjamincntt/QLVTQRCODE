namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyTicketListResponseItem
{
    public string? MaPhieu { get; set; }

    public string? TenPhieu { get; set; }

    public string? MoTa { get; set; }
    
    public DateTime? NgayThem { get; set; }
    
    public byte? TrangThai { get; set; }
}