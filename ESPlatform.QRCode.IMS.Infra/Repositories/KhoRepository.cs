using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class KhoRepository : EfCoreRepositoryBase<QlvtKho, AppDbContext>, IKhoRepository
{
    public KhoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListWarehousesAsync()
    {
        var query = DbContext.QlvtKhos
            .OrderBy(x => x.OrganizationName)
            .Select(x => new
            {
                x.OrganizationId,
                x.OrganizationName
            });
        return await query.ToListAsync();
    }
}