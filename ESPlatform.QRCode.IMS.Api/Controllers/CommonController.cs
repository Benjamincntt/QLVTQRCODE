using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.ViTris.Responses;
using ESPlatform.QRCode.IMS.Core.Services.Common;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class CommonController: ApiControllerBase
{
    private readonly ICommonService _commonService;

    public CommonController(ICommonService commonService)
    {
        _commonService = commonService;
    }

    /// <summary>
    /// Cập nhật vị trí vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="khoId"></param>
    /// <param name="request"></param>
    /// <param name="idViTri"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/{khoId:int}/update-supply-location")]
    public async Task<int> ModifySuppliesLocationAsync(int vatTuId, int khoId, [FromBody]ModifiedSuppliesLocationRequest request) {
        return await _commonService.ModifySuppliesLocationAsync(vatTuId, khoId, request);
    }

    /// <summary>
    /// Cập nhật ảnh vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="currentImagePath"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/update-supplies-image")]
    public async Task<string> ModifySuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file)
    {
        return await _commonService.ModifySuppliesImageAsync(vatTuId, currentImagePath, file);
    }
    
    /// <summary>
    /// Thêm ảnh vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("{vatTuId:int}/create-supplies-image")]
    public async Task<string> CreateSuppliesImageAsync(int vatTuId, IFormFile file)
    {
        return await _commonService.CreateSuppliesImageAsync(vatTuId, file);
    }
    
    /// <summary>
    /// Xóa ảnh vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="currentImagePath"></param>
    /// <returns></returns>
    [HttpDelete("{vatTuId:int}/delete-supplies-image")]
    public async Task<int> DeleteSuppliesImageAsync(int vatTuId, string currentImagePath)
    {
        return await _commonService.DeleteSuppliesImageAsync(vatTuId, currentImagePath);
    }

    /// <summary>
    /// Danh sách vị trí của vật tư theo cấp
    /// </summary>
    /// <param name="parentId"> 0: Tất cả vị trí, 1: nhóm 8, 2: nhóm 9, 3: nhóm 10</param>
    /// <returns></returns>
    [HttpGet("{parentId:int}")]
    public async Task<IEnumerable<SupplyLocationListResponseItem>> ListSuppliesLocationAsync(int parentId)
    {
        return await _commonService.ListSuppliesLocationAsync(parentId);
    }
    
    
}