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

    public async Task<PagedList<dynamic>> ListSupplyTicketAsync(string keywords, SupplyTicketStatus status, int pageIndex, int pageSize)
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuats
            .Join(DbContext.TbCauHinhTrangThais,
                x => x.TrangThai,
                y => y.GiaTri,
                (x, y) => new { PhieuDeXuat = x, CauHinhTrangThai = y})
            .Where(x => x.PhieuDeXuat.TrangThai != (int?)SupplyTicketStatus.Deleted)
            .Where(x => status == SupplyTicketStatus.Unknown || x.PhieuDeXuat.TrangThai == (byte)status)
            .Where(x => string.IsNullOrWhiteSpace(keywords)
                        || (x.PhieuDeXuat.TenPhieu != null && x.PhieuDeXuat.TenPhieu.ToLower().Contains(keywords))
                        || (x.PhieuDeXuat.MoTa != null && x.PhieuDeXuat.MoTa.ToLower().Contains(keywords)))
            .OrderByDescending(x => x.PhieuDeXuat.NgayThem)
            .ThenBy(x => x.PhieuDeXuat.TrangThai)
            .Select(x => new
            {
                x.PhieuDeXuat.Id,
                NgayThem = x.PhieuDeXuat.NgayThem ?? null,
                TenPhieu = x.PhieuDeXuat.TenPhieu ?? string.Empty,
                MaPhieu = x.PhieuDeXuat.MaPhieu ?? string.Empty,
                MoTa = x.PhieuDeXuat.MoTa ?? string.Empty,
                TrangThai = x.PhieuDeXuat.TrangThai ?? (int?)SupplyTicketStatus.Unsigned,
                TenTrangThai = x.CauHinhTrangThai.TenTrangThai ?? string.Empty,
                MaMau = x.CauHinhTrangThai.MaMau ?? string.Empty,
            });
        return await query.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }

    public async Task<IEnumerable<dynamic>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds)
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuatDetails
            .Join(DbContext.QlvtMuaSamPhieuDeXuats,
                x => x.PhieuDeXuatId,
                y=> y.Id,
                (x, y) => new { PhieuDeXuatDetail = x, PhieuDeXuat = y})
            .Where(x => x.PhieuDeXuat.TrangThai == (int)SupplyTicketStatus.Unsigned)
            .Where(x => x.PhieuDeXuatDetail.VatTuId != null && vatTuIds.Contains((int)x.PhieuDeXuatDetail.VatTuId))
            .GroupBy(st => st.PhieuDeXuatDetail.VatTuId)
            .Select(x => new
            {
                VatTuId = x.Key,
                TotalQuantity = x.Sum(st => st.PhieuDeXuatDetail.SoLuong)
            });
            return await query.ToListAsync();
    }
}