using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class ViTriRepository : EfCoreRepositoryBase<QlvtViTri, AppDbContext>, IViTriRepository
{
    public ViTriRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListSuppliesLocationAsync(int parentId)
    {
        var query = DbContext.QlvtViTris
            .Where(x => parentId == 0 || x.ParentId == parentId)
            .Select(x => new
            {
                LocationId = x.Id,
                LocationName = x.Ten
            });
        return await query.ToListAsync();
    }
}