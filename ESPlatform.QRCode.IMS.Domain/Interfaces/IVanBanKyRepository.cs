using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IVanBanKyRepository : IRepositoryBase<QlvtVanBanKy> {
    Task<List<string?>> ListVanbanKyUrlAsync(int phieuId);
}