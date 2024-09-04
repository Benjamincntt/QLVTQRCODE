using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Services.KiemKe;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class KiemKeController : ApiControllerBase
{
    private readonly IKiemKeService _kiemKeService;

    public KiemKeController(IKiemKeService kiemKeService)
    {
        _kiemKeService = kiemKeService;
    }
    
    /// <summary>
    /// Hiển thị chi tiết kiểm kê: Quét QR Code kiểm kê/ Nhập mã tay
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <returns></returns>
    [HttpGet("{vatTuId:int}")]
    public async Task<InventoryCheckResponse> GetAsync(int vatTuId)
    {
        return await _kiemKeService.GetAsync(vatTuId);
    }
    
    /// <summary>
    /// Cập nhật vị trí vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="idViTri"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/{idViTri:int}")]
    public async Task<int> ModifySuppliesLocationAsync(int vatTuId, int idViTri, [FromBody]ModifiedSuppliesLocationRequest request) {
        return await _kiemKeService.ModifySuppliesLocationAsync(vatTuId, idViTri, request);
    }
    
    /// <summary>
    /// Cập nhật ảnh vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="imageUrl"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/update-supplies-image")]
    public async Task<int> ModifiedSuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file)
    {
        return await _kiemKeService.ModifiedSuppliesImageAsync(vatTuId, currentImagePath, file);
    }
    
}