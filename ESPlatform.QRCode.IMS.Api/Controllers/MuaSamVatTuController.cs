using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Domain.Entities;
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
    /// Danh sách vật tư để chọn mua
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
    /// <param name="id"> VatTuId / VatTuNewId </param>
    /// <param name="isSystemSupply"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/chi-tiet-vat-tu")]
    public async Task<SupplyOrderDetailResponse> GetPurchaseSupplyAsync(int id, bool isSystemSupply = true)
    {
        return await _muaSamVatTuService.GetPurchaseSupplyAsync(id, isSystemSupply);
    }
    
    
    /// <summary>
    /// Thêm mới phiếu cung ứng
    /// </summary>
    /// <returns>Id phiếu cung ứng</returns>
    [HttpPost("them-phieu-cung-ung")]
    public async Task<int> CreateSupplyTicketAsync([FromBody]CreatedSupplyTicketRequest request)
    {
        return await _muaSamVatTuService.CreateSupplyTicketAsync(request);
    }
    
    /// <summary>
    /// Danh sách Phiếu cung ứng
    /// </summary>
    /// <returns></returns>
    [HttpGet("list-phieu-cung-ung")]
    public async Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync([FromQuery] PhraseAndPagingFilter request )
    {
        return await _muaSamVatTuService.ListSupplyTicketAsync(request);
    }
    
    /// <summary>
    /// Hiển thị thông tin chi tiết phiếu cung ứng
    /// </summary>
    /// <param name="supplyTicketId"></param>
    /// <returns></returns>
    [HttpGet("{supplyTicketId:int}/chi-tiet-phieu-cung-ung")]
    public async Task<SupplyTicketDetailResponse> GetSupplyTicketDetailAsync(int supplyTicketId)
    {
        return await _muaSamVatTuService.GetSupplyTicketDetailAsync(supplyTicketId);
    }

    /// <summary>
    /// Xóa một phiếu cung ứng
    /// </summary>
    /// <param name="supplyTicketId"></param>
    /// <returns></returns>
    [HttpDelete("{supplyTicketId:int}/xoa-phieu-cung-ung")]
    public async Task<int> DeleteSupplyTicketAsync(int supplyTicketId)
    {
        return await _muaSamVatTuService.DeleteSupplyTicketAsync( supplyTicketId);
    }

    /// <summary>
    /// Danh sách các kho hàng
    /// </summary>
    /// <returns></returns>
    [HttpGet("danh-sach-kho")]
    public async Task<IEnumerable<WarehouseResponseItem>> ListWarehousesAsync()
    {
        return await _muaSamVatTuService.ListWarehousesAsync();
    }
}