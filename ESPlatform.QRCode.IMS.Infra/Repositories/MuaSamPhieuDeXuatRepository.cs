using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class MuaSamPhieuDeXuatRepository : EfCoreRepositoryBase<QlvtMuaSamPhieuDeXuat, AppDbContext>, IMuaSamPhieuDeXuatRepository
{
    public MuaSamPhieuDeXuatRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListSupplyTicketAsync()
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuats
            .OrderByDescending(x => x.NgayThem)
            .ThenBy(x => x.TrangThai)
            .Select(x => new
            {
                x.NgayThem,
                x.TenPhieu,
                x.MoTa,
                x.TrangThai,
            });
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<dynamic>> ListSupplyTicketByTimeAsync(DateTime startTime, DateTime endTime)
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuats
            .Where(x => x.NgayThem >= startTime && x.NgayThem <= endTime)
            .OrderByDescending(x => x.NgayThem)
            .ThenBy(x => x.TrangThai)
            .Select(x => new
            {
                x.NgayThem,
                x.TenPhieu,
                x.MoTa,
                x.TrangThai,
            });
        return await query.ToListAsync();
    }
}