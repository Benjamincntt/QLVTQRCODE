using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public interface IKiemKeService
{
    Task<InventoryCheckResponse> GetAsync(int vatTuId);

    Task<int> ModifySuppliesLocationAsync(int vatTuId, ModifiedSuppliesLocationRequest request);
}