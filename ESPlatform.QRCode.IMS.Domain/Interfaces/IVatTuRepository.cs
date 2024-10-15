using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IVatTuRepository : IRepositoryBase<QlvtVatTu> {
    Task<dynamic?> GetInventoryCheckInformationAsync(int vatTuId, int kyKiemKeId);
    Task<IEnumerable<dynamic>> GetPositionAsync(int vatTuId);
    Task<dynamic?> GetLotNumberAsync(int vatTuId, int khoId);
    Task<PagedList<dynamic>> ListAsync( string tenVatTu, string maVatTu, int idKho, int idToMay, int idGiaKe, int idNgan, string maNhom, string relativeBasePath, int pageIndex, int pageSize);
    Task<dynamic?> GetWarehouseIdAsync(int vatTuId);
}