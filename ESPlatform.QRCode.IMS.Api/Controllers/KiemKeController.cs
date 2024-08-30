using ESPlatform.QRCode.IMS.Api.Controllers.Base;
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
    
}