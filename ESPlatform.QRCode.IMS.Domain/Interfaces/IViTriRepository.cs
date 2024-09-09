using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IViTriRepository : IRepositoryBase<QlvtViTri>
{
    Task<IEnumerable<dynamic>> ListSuppliesLocationAsync(int parentId);
}