using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class MuaSamPhieuDeXuatRepository : EfCoreRepositoryBase<QlvtMuaSamPhieuDeXuat, AppDbContext>, IMuaSamPhieuDeXuatRepository
{
    public MuaSamPhieuDeXuatRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<dynamic>> ListSupplyTicketAsync(string keywords, int pageIndex, int pageSize)
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuats
            .Where(x => x.TrangThai != (int?)SupplyTicketStatus.Deleted)
            .Where(x => string.IsNullOrWhiteSpace(keywords)
                        || x.TenPhieu == null 
                        || x.TenPhieu.ToLower().Contains(keywords)
                        || x.MoTa == null
                        || x.MoTa.ToLower().Contains(keywords))
            .OrderByDescending(x => x.NgayThem)
            .ThenBy(x => x.TrangThai)
            .Select(x => new
            {
                x.Id,
                x.NgayThem,
                x.TenPhieu,
                x.MoTa,
                x.TrangThai,
            });
        return await query.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }
}