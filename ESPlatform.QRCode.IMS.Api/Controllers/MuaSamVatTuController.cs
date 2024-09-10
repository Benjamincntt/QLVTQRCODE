using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

[Route("/api/v1/mua-sam-vat-tu")]
public class MuaSamVatTuController : ApiControllerBase
{
    private readonly IMuaSamVatTuService _muaSamVatTuService;

    public MuaSamVatTuController(IMuaSamVatTuService muaSamVatTuService)
    {
        _muaSamVatTuService = muaSamVatTuService;
    }

    /// <summary>
    /// Danh sách vật tư
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync([FromQuery]SupplyListRequest request)
    {
        return await _muaSamVatTuService.ListVatTuAsync(request);
    }

    /// <summary>
    /// Chi tiết vật tư mua sắm
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <returns></returns>
    [HttpGet("{vatTuId:int}/chi-tiet-vat-tu")]
    public async Task<PurchaseSupplyResponse> GetPurchaseSupplyAsync(int vatTuId)
    {
        return await _muaSamVatTuService.GetPurchaseSupplyAsync(vatTuId);
    }
    
    /// <summary>
    /// Thêm mới vật tư 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("them-moi-vat-tu")]
    public async Task<int> CreateSupplyAsync([FromBody] CreatedSupplyRequest request)
    {
        return await _muaSamVatTuService.CreateSupplyAsync(request);
    }
}