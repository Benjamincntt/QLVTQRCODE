using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamPhieuDeXuatRepository : IRepositoryBase<QlvtMuaSamPhieuDeXuat>
{
    Task<PagedList<dynamic>> ListSupplyTicketAsync(string keywords, SupplyTicketStatus status, int pageIndex, int pageSize);
    Task<IEnumerable<dynamic>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds);
}