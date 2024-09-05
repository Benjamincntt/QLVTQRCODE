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