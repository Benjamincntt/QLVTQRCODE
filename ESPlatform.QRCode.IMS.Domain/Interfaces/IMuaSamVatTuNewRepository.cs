using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamVatTuNewRepository : IRepositoryBase<QlvtMuaSamVatTuNew>
{
    Task<PagedList<dynamic>> ListAsync( string tenVatTu, string maVatTu, int pageIndex, int pageSize);
}