﻿using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;
using ESPlatform.QRCode.IMS.Core.Services.GioHang;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class GioHangController : ApiControllerBase
{
    private readonly IGioHangService _gioHangService;

    public GioHangController(IGioHangService gioHangService)
    {
        _gioHangService = gioHangService;
    }

    /// <summary>
    /// Đếm số vật tư trong giỏ hàng
    /// </summary>
    /// <returns></returns>
    [HttpGet("count")]
    public async Task<int> GetSupplyCountAsync()
    {
        return await _gioHangService.GetSupplyCountAsync();
    }

    /// <summary>
    /// Danh sách vật tư trong giỏ hàng
    /// </summary>
    /// <returns></returns>
    [HttpGet("danh-sach-vat-tu")]
    public async Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync()
    {
        return await _gioHangService.ListSupplyAsync();
    }

    /// <summary>
    /// Tăng giảm số lượng của một vật tư trong giỏ hàng
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="quantity"> Số lượng vật tư </param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}")]
    public async Task<int> ModifyQuantityAsync(int vatTuId, int quantity = 0)
    {
        return await _gioHangService.ModifyQuantityAsync(vatTuId, quantity);
    }

    /// <summary>
    /// Cập nhật thông số kỹ thuật và ghi chú của 1 vật tư trong giỏ hàng
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("{vatTuId:int}/thong-tin")]
    public async Task<int> ModifyInformationAsync(int vatTuId, [FromBody] ModifiedCartSupplyRequest request)
    {
        return await _gioHangService.ModifyInformationAsync(vatTuId, request);
    }
    
    /// <summary>
    /// Xóa 1 vật tư trong giỏ hàng
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <returns></returns>
    [HttpDelete("{vatTuId:int}")]
    public async Task<int> DeleteSupplyAsync(int vatTuId)
    {
        return await _gioHangService.DeleteSupplyAsync(vatTuId);
    }

    /// <summary>
    /// Thêm mới 1 vật tư vào giỏ hàng
    /// </summary>
    /// <param name="vatTuId"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("{vatTuId:int}")]
    public async Task<int> CreateSupplyAsync(int vatTuId,[FromBody] CreatedCartSupplyRequest request)
    {
        return await _gioHangService.CreateSupplyAsync(vatTuId, request);
    }
}