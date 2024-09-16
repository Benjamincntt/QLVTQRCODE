using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class MuaSamPhieuDeXuatDetailRepository : EfCoreRepositoryBase<QlvtMuaSamPhieuDeXuatDetail, AppDbContext>, IMuaSamPhieuDeXuatDetailRepository
{
    public MuaSamPhieuDeXuatDetailRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}