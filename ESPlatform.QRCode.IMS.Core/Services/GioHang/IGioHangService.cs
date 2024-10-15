using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;

namespace ESPlatform.QRCode.IMS.Core.Services.GioHang;

public interface IGioHangService
{
    Task<int> GetSupplyCountAsync();
    Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync();
    Task<int> ModifyQuantityAsync(int gioHangId, int quantity);
    Task<int> DeleteSupplyAsync(int gioHangId);
    Task<int> ModifyInformationAsync(int gioHangId, ModifiedCartSupplyRequest request);
    Task<int> CreateCartSupplyAsync(int vatTuId, CreatedCartSupplyRequest request);
    Task<int> CreateSupplyAsync(CreatedSupplyRequest request);
}