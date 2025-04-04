﻿using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Services.KiemKe;
using ESPlatform.QRCode.IMS.Domain.Entities;
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
    /// <param name="khoId"></param>
    /// <returns></returns>
    [HttpGet("{maVatTu}/{khoId:int}")]
    public async Task<InventoryCheckResponse> GetAsync(string maVatTu, int khoId)
    {
        return await _kiemKeService.GetAsync(maVatTu, khoId);
    }

    /// <summary>
    /// Cập nhật DFF vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="kyKiemKeId"></param>
    /// <param name="kyKiemKeChiTietId"></param>
    /// <param name="soLuongKiemKe"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/{kyKiemKeId:int}/{kyKiemKeChiTietId:int}/update-supplies-dff")]
    public async Task<int> ModifySuppliesDffAsync(int vatTuId, int kyKiemKeId, int kyKiemKeChiTietId, decimal soLuongKiemKe, [FromBody] ModifiedSuppliesDffRequest request)
    {
        return await _kiemKeService.ModifySuppliesDffAsync(vatTuId, kyKiemKeId, kyKiemKeChiTietId, soLuongKiemKe, request);
    }

    /// <summary>
    /// Cập nhật QTY vật tư
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="kyKiemKeId"></param>
    /// <param name="soLuongKiemKe"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/{kyKiemKeId:int}/update-supplies-qty")]
    public async Task<int> ModifySuppliesQtyAsync(int vatTuId, int kyKiemKeId, decimal soLuongKiemKe)
    {
        return await _kiemKeService.ModifySuppliesQtyAsync(vatTuId, kyKiemKeId, soLuongKiemKe);
    }

    /// <summary>
    /// Danh sách kỳ kiểm kê
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IEnumerable<InventoryCheckListResponseItem>> ListAsync()
    {
        return await _kiemKeService.ListAsync();
    }
    
    /// <summary>
    /// Hiển thị kỳ kiểm kê hiện tại
    /// </summary>
    /// <returns></returns>
    [HttpGet("current-inventory-check")]
    public async Task<QlvtKyKiemKe> GetCurrentInventoryCheckAsync()
    {
        return await _kiemKeService.GetCurrentInventoryCheckAsync();
    }
}