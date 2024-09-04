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
    /// <param name="maVatTu"></param>
    /// <returns></returns>
    [HttpGet("{maVatTu}")]
    public async Task<InventoryCheckResponse> GetAsync(string maVatTu)
    {
        return await _kiemKeService.GetAsync(maVatTu);
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
    /// <param name="currentImagePath"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/update-supplies-image")]
    public async Task<int> ModifySuppliesImageAsync(int vatTuId, string currentImagePath, IFormFile file)
    {
        return await _kiemKeService.ModifySuppliesImageAsync(vatTuId, currentImagePath, file);
    }
    
    /// <summary>
    /// Thêm ảnh vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost("{vatTuId:int}/create-supplies-image")]
    public async Task<int> CreateSuppliesImageAsync(int vatTuId, IFormFile file)
    {
        return await _kiemKeService.CreateSuppliesImageAsync(vatTuId, file);
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
        return await _kiemKeService.DeleteSuppliesImageAsync(vatTuId, currentImagePath);
    }

    /// <summary>
    /// Cập nhật DFF vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="kyKiemKeChiTietId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/{kyKiemKeChiTietId:int}/update-supplies-dff")]
    public async Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeChiTietId,[FromBody] ModifiedSuppliesDffRequest request)
    {
        return await _kiemKeService.ModifySuppliesDffAsync(vatTuId,kyKiemKeChiTietId, request);
    }
    
}