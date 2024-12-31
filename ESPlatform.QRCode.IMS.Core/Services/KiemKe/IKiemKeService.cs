using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Domain.Entities;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public interface IKiemKeService
{
    Task<InventoryCheckResponse> GetAsync(string maVatTu, int khoId);

    Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeId, int kyKiemKeChiTietId, decimal soLuongKiemKe,
        ModifiedSuppliesDffRequest request);

    Task<int> ModifySuppliesQtyAsync(int vatTuId, int kyKiemKeId, decimal soLuongKiemKe);
    Task<IEnumerable<InventoryCheckListResponseItem>> ListAsync();
    Task<QlvtKyKiemKe> GetCurrentInventoryCheckAsync();
}