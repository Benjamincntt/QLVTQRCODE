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
    /// <param name="id">VatTuId / VatTuNewId</param>
    /// <param name="isSystemSupply"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/chi-tiet-vat-tu")]
    public async Task<PurchasedSupplyResponse> GetPurchaseSupplyAsync(int id, bool isSystemSupply = true)
    {
        return await _muaSamVatTuService.GetPurchaseSupplyAsync(id, isSystemSupply);
    }
    
    /// <summary>
    /// Thêm mới vật tư không có trong hệ thống 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("them-moi-vat-tu")]
    public async Task<int> CreateSupplyAsync([FromBody] CreatedSupplyRequest request)
    {
        return await _muaSamVatTuService.CreateSupplyAsync(request);
    }
    
    /// <summary>
    /// Thêm mới phiếu cung ứng
    /// </summary>
    /// <returns>Id phiếu cung ứng</returns>
    [HttpPost("them-phieu-cung-ung")]
    public async Task<int> CreateSupplyTicketAsync()
    {
        return await _muaSamVatTuService.CreateSupplyTicketAsync();
    }
    
    /// <summary>
    /// Danh sách Phiếu cung ứng
    /// </summary>
    /// <returns></returns>
    [HttpGet("list-phieu-cung-ung")]
    public async Task<IEnumerable<SupplyTicketListResponseItem>> ListSupplyTicketAsync()
    {
        return await _muaSamVatTuService.ListSupplyTicketAsync();
    }
    
    /// <summary>
    /// Thêm nhiều vật tư vào phiếu đề xuất
    /// </summary>
    /// <param name="supplyTicketId"> Id phiếu đề xuất </param>
    /// <param name="requests"> danh sách các vật tư được chọn </param>
    /// <returns> Số bản ghi được thêm </returns>
    [HttpPost("{supplyTicketId:int}/them-nhieu-vat-tu")]
    public async Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests)
    {
        return await _muaSamVatTuService.CreateManySupplyTicketDetailAsync(supplyTicketId, requests);
    }
    
}