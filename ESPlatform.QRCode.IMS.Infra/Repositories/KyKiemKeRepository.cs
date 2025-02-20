using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class KyKiemKeRepository : EfCoreRepositoryBase<QlvtKyKiemKe, AppDbContext>, IKyKiemKeRepository
{
    public KyKiemKeRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListInputAsync()
    {
        return await DbContext.QlvtKyKiemKes
            .OrderBy(x => x.PhysicalInventoryName)
            .Select(x => new
            {
                KyKiemKeId = x.PhysicalInventoryId ?? 0,
                TenKyKiemKe = x.PhysicalInventoryName
            })
            .ToListAsync();
    }
}