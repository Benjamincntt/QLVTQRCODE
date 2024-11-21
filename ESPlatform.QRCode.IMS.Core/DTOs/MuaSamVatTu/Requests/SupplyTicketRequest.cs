using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

public class SupplyTicketRequest : PhraseAndPagingFilter
{
    public SupplyTicketStatus? SupplyTicketStatus { get; set; } 
}