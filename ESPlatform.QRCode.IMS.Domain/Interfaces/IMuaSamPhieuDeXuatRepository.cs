using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamPhieuDeXuatRepository : IRepositoryBase<QlvtMuaSamPhieuDeXuat>
{
    Task<PagedList<dynamic>> ListSupplyTicketAsync(string keywords, int pageIndex, int pageSize);
}