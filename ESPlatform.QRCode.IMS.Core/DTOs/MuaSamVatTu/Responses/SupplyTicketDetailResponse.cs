namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;

public class SupplyTicketDetailResponse
{
    public string TenPhieu { get; set; } = string.Empty;

    public string MoTa { get; set; } = string.Empty;

    public List<SupplyResponse> DanhSachVatTu { get; set; } = new ();

    public int Tong { get; set; }
    
    public bool IsEditable  { get; set; }
}