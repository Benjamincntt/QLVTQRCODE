using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.ViTris.Responses;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Services.Common;

public interface ICommonService
{
    Task<int> ModifySuppliesLocationAsync(int vatTuId, ModifiedSuppliesLocationRequest request);
    
    Task<string> ModifySuppliesImageAsync(int vatTuId, string inputPath, IFormFile file);
    
    Task<string> CreateSuppliesImageAsync(int vatTuId, IFormFile file);
    
    Task<int> DeleteSuppliesImageAsync(int vatTuId, string inputPath);
    Task<IEnumerable<SupplyLocationListResponseItem>> ListSuppliesLocationAsync(int parentId);
}