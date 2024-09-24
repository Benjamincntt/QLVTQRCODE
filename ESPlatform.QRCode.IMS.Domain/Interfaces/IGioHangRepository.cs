using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IGioHangRepository: IRepositoryBase<QlvtGioHang>
{
    Task<IEnumerable<dynamic>> ListSupplyAsync(int userId);
}