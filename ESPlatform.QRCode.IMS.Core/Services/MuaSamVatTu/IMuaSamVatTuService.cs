using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public interface IMuaSamVatTuService
{
    Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request);
    Task<SupplyOrderDetailResponse> GetSupplyOrderDetailAsync(int id, bool isSystemSupply);
    Task<int> ProcessSupplyTicketCreationAsync(ProcessSupplyTicketCreationRequest request);
    Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync(SupplyTicketRequest request);
    // Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests);
    Task<SupplyTicketDetailResponse> GetSupplyTicketDetailAsync(int supplyTicketId);
    Task<int> DeleteSupplyTicketAsync(int supplyTicketId);
    Task<IEnumerable<WarehouseResponseItem>> ListWarehousesAsync();
    Task<IEnumerable<QlvtVatTuBoMa>> ListGroupCodeAsync();
    Task<int> CountSupplyTicketsByStatusAsync(SupplyTicketStatus status);
    Task<IEnumerable<CreatedSupplyTicketWarningResponseItem>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds);
}