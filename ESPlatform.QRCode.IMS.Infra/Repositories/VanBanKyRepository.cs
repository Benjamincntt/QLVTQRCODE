using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class VanBanKyRepository : EfCoreRepositoryBase<QlvtVanBanKy, AppDbContext>, IVanBanKyRepository
{
    public VanBanKyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}