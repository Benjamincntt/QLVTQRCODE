using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Exceptions;
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
    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync([FromQuery] SupplyListRequest request)
    {
        return await _muaSamVatTuService.ListVatTuAsync(request);
    }

    /// <summary>
    /// Chi tiết vật tư mua sắm
    /// </summary>
    /// <param name="id"> VatTuId / VatTuNewId </param>
    /// <param name="khoId"></param>
    /// <param name="isSystemSupply"></param>
    /// <returns></returns>
    [HttpGet("{id:int}/{khoId:int}/chi-tiet-vat-tu")]
    public async Task<SupplyOrderDetailResponse> GetSupplyOrderDetailAsync(int id, int khoId, bool isSystemSupply = true)
    {
        return await _muaSamVatTuService.GetSupplyOrderDetailAsync(id, khoId, isSystemSupply);
    }


    /// <summary>
    /// Lập mới phiếu cung ứng
    /// </summary>
    /// <returns>Id phiếu cung ứng</returns>
    [HttpPost("them-phieu-cung-ung")]
    public async Task<int> ProcessSupplyTicketCreationAsync(
        [FromBody] ProcessSupplyTicketCreationRequest creationRequest)
    {
        return await _muaSamVatTuService.ProcessSupplyTicketCreationAsync(creationRequest);
    }

    /// <summary>
    /// Danh sách Phiếu cung ứng
    /// </summary>
    /// <returns></returns>
    [HttpGet("list-phieu-cung-ung")]
    public async Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync(
        [FromQuery] SupplyTicketRequest request)
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
        return await _muaSamVatTuService.DeleteSupplyTicketAsync(supplyTicketId);
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

    /// <summary>
    /// Danh sách nhóm
    /// </summary>
    /// <returns></returns>
    [HttpGet("danh-sach-nhom")]
    public async Task<IEnumerable<QlvtVatTuBoMa>> ListGroupCodeAsync()
    {
        return await _muaSamVatTuService.ListGroupCodeAsync();
    }

    /// <summary>
    /// Đếm số phiếu theo trạng thái
    /// </summary>
    /// <param name="status"></param> 
    /// <returns></returns>
    [HttpGet("so-phieu")]
    public async Task<int> CountSupplyTicketsByStatusAsync(SupplyTicketStatus status = SupplyTicketStatus.Unsigned)
    {
        return await _muaSamVatTuService.CountSupplyTicketsByStatusAsync(status);
    }

    /// <summary>
    /// Cảnh báo số lượng vật tư trong các phiếu khác đang chờ duyệt
    /// </summary>
    /// <param name="vatTuIds"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    [HttpGet("canh-bao-them-phieu")]
    public async Task<ActionResult<IEnumerable<string>>> ListCreatedSupplyTicketWarningAsync([FromQuery] List<int> vatTuIds)
    {
        if (!vatTuIds.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }

        var warnings = await _muaSamVatTuService.ListCreatedSupplyTicketWarningAsync(vatTuIds);
        return Ok(warnings);
    }
}