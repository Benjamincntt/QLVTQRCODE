using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public interface IMuaSamVatTuService
{
    Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request);
    Task<SupplyOrderDetailResponse> GetPurchaseSupplyAsync(int id, bool isSystemSupply);
    Task<int> CreateSupplyAsync(CreatedSupplyRequest request);
    Task<int> CreateSupplyTicketAsync(string moTa, List<SupplyTicketDetailRequest> requests);
    Task<IEnumerable<SupplyTicketListResponseItem>> ListSupplyTicketAsync(DateTime? date);
    // Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests);
    Task<SupplyTicketDetailResponse> GetSupplyTicketDetailAsync(int supplyTicketId);
    Task<int> DeleteSupplyTicketAsync(int supplyTicketId);
}