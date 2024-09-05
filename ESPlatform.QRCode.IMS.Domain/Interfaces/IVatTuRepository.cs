using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IVatTuRepository : IRepositoryBase<QlvtVatTu> {
    Task<dynamic?> GetInventoryCheckInformationAsync(int vatTuId, int kyKiemKeId);
    Task<IEnumerable<dynamic>> GetPositionAsync(int vatTuId);
    Task<dynamic?> GetInventoryAsync(int vatTuId, int khoId);
}