using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public interface IKiemKeService
{
    Task<InventoryCheckResponse> GetAsync(string maVatTu);

    Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeId, int kyKiemKeChiTietId, int soLuongKiemKe, ModifiedSuppliesDffRequest request);
    Task<int> ModifySuppliesQtyAsync(int vatTuId, int kyKiemKeId, int soLuongKiemKe);
    Task<IEnumerable<InventoryCheckListResponseItem>> ListAsync();
}