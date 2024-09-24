using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;

namespace ESPlatform.QRCode.IMS.Core.Services.GioHang;

public interface IGioHangService
{
    Task<int> GetSupplyCountAsync();
    Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync();
    Task<int> ModifyQuantityAsync(int vatTuId, int quantity);
    Task<int> DeleteSupplyAsync(int vatTuId);
    Task<int> ModifyInformationAsync(int vatTuId, ModifiedCartSupplyRequest request);
    Task<int> CreateSupplyAsync(int vatTuId, CreatedCartSupplyRequest request);
}