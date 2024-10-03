namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class ProcessSupplyTicketCreationRequest
{
    public string? Description { get; set; }
    public List<SupplyTicketDetailRequest> SupplyTicketDetails { get; set; } = new List<SupplyTicketDetailRequest>();
}