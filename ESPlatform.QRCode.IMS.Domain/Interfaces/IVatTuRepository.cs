using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IVatTuRepository : IRepositoryBase<QlvtVatTu> {
    Task<dynamic?> GetInventoryCheckInformationAsync(int vatTuId, int kyKiemKeId);
    Task<IEnumerable<dynamic>> GetPositionAsync(int vatTuId);
    Task<dynamic?> GetLotNumberAsync(int vatTuId, int khoId);
    Task<PagedList<dynamic>> ListAsync( 
        string tenVatTu, 
        string maVatTu, 
        int idKho,
        List<int>? idToMay,
        List<int>? idGiaKe,
        List<int>? idNgan,
        List<string>? listMaNhom,
        List<int>? listVatTuTonKhoIds,
        int pageIndex,
        int pageSize);
    Task<dynamic?> GetWarehouseIdAsync(int vatTuId);
}