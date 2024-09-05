using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.Common;

public interface ICommonService
{
    Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri, ModifiedSuppliesLocationRequest request);
    
    Task<int> ModifySuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file);
    
    Task<int> CreateSuppliesImageAsync(int vatTuId, IFormFile file);
    
    Task<int> DeleteSuppliesImageAsync(int vatTuId, string currentImagePath);
}