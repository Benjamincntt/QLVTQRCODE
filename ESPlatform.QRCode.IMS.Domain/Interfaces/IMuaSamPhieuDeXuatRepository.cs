using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamPhieuDeXuatRepository : IRepositoryBase<QlvtMuaSamPhieuDeXuat>
{
    Task<IEnumerable<dynamic>> ListSupplyTicketAsync();
    Task<IEnumerable<dynamic>> ListSupplyTicketByTimeAsync(DateTime startTime, DateTime endTime);
}