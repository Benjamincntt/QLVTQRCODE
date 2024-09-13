using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.LapPhieu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public interface IMuaSamVatTuService
{
    Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request);
    Task<PurchasedSupplyResponse> GetPurchaseSupplyAsync(int id, bool isVatTu);
    Task<int> CreateSupplyAsync(CreatedSupplyRequest request);
    Task<int> CreatePurchaseOrderAsync(CreatedPurchaseOrderRequest request);
}