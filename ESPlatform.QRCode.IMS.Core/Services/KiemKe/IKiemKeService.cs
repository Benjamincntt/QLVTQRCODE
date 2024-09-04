using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.KiemKe;

public interface IKiemKeService
{
    Task<InventoryCheckResponse> GetAsync(int vatTuId);

    Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri, ModifiedSuppliesLocationRequest request);
    Task<int> ModifySuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file);
    Task<int> CreateSuppliesImageAsync(int vatTuId, IFormFile file);
    Task<int> DeleteSuppliesImageAsync(int vatTuId, string currentImagePath);
}