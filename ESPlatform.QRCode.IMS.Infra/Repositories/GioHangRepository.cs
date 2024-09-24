using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class GioHangRepository : EfCoreRepositoryBase<QlvtGioHang, AppDbContext>, IGioHangRepository
{
    public GioHangRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListSupplyAsync(int userId)
    {
        var query = DbContext.QlvtGioHangs
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.TenVatTu)
            .Select(x => new
            {
                x.VatTuId,
                x.TenVatTu,
                x.SoLuong,
                x.ThongSoKyThuat,
                x.GhiChu,
                x.Image
            });
        return await query.ToListAsync();
    }
}