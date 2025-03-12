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

    public async Task<PagedList<dynamic>> ListSupplyTicketAsync(
        string keywords,
        SupplyTicketStatus status,
        int userId,
        string maDoiTuongKy,
        List<int> phieuDeXuatOtherIds,
        int pageIndex, int pageSize)
    {
        var query = DbContext.QlvtMuaSamPhieuDeXuats
            .Join(DbContext.TbCauHinhTrangThais, 
                x => x.TrangThai,
                y => y.GiaTri,
                (x, y) => new { PhieuDeXuat = x, CauHinhTrangThai = y})
            .GroupJoin(DbContext.QlvtMuaSamPdxKies
                    .Where(pdx => pdx.MaDoiTuongKy == maDoiTuongKy), 
                x => x.PhieuDeXuat.Id,
                y => y.PhieuDeXuatId,
                (x, pdxKies) => new { x.PhieuDeXuat, x.CauHinhTrangThai, PdxKies = pdxKies })
            .SelectMany(x => x.PdxKies.DefaultIfEmpty(), (x, pdxKy) => new { x.PhieuDeXuat, x.CauHinhTrangThai, PdxKy = pdxKy })
            .Where(x => userId == 0 || maDoiTuongKy != "NguoiLap" || x.PhieuDeXuat.MaNguoiThem == userId)
            .Where(x => status == SupplyTicketStatus.Unknown || x.PhieuDeXuat.TrangThai == (byte)status)
            .Where(x => string.IsNullOrWhiteSpace(keywords)
                        || (x.PhieuDeXuat.TenPhieu != null && x.PhieuDeXuat.TenPhieu.ToLower().Contains(keywords))
                        || (x.PhieuDeXuat.MoTa != null && x.PhieuDeXuat.MoTa.ToLower().Contains(keywords)))
            .Where(x => !phieuDeXuatOtherIds.Any() || phieuDeXuatOtherIds.Contains(x.PhieuDeXuat.Id))
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
                IsKySo = x.PdxKy != null && x.PdxKy.TrangThai == null && userId != 0,
                IsBoQua = maDoiTuongKy == "KiemSoatAT" && x.PdxKy != null && x.PdxKy.TrangThai == null && userId != 0,
            });
        return await query.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }

    public async Task<IEnumerable<dynamic>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds)
    {
        var listSupplies = DbContext.QlvtMuaSamPhieuDeXuatDetails
            
            .Join(DbContext.QlvtMuaSamPhieuDeXuats,
                x => x.PhieuDeXuatId,
                y=> y.Id,
                (x, y) => new { PhieuDeXuatDetail = x, PhieuDeXuat = y})
            .Join(DbContext.QlvtVatTus,
                x => x.PhieuDeXuatDetail.VatTuId,
                y => y.VatTuId,
                (x, y) => new { x.PhieuDeXuatDetail, x.PhieuDeXuat, VatTu = y })
            .Where(x => x.PhieuDeXuat.TrangThai == (int)SupplyTicketStatus.Unsigned)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == true)
            .Where(x => x.PhieuDeXuatDetail.VatTuId != null && vatTuIds.Contains((int)x.PhieuDeXuatDetail.VatTuId))
            .Select(x => new
            {
                TenVatTu = x.VatTu.TenVatTu ?? string.Empty,
                TenPhieu = x.PhieuDeXuat.TenPhieu ?? string.Empty,
                SoLuong = x.PhieuDeXuatDetail.SoLuong ?? 0,
            });
        
        var listSuppliesNew = DbContext.QlvtMuaSamPhieuDeXuatDetails
            
            .Join(DbContext.QlvtMuaSamPhieuDeXuats,
                x => x.PhieuDeXuatId,
                y=> y.Id,
                (x, y) => new { PhieuDeXuatDetail = x, PhieuDeXuat = y})
            .Join(DbContext.QlvtMuaSamVatTuNews,
                x => x.PhieuDeXuatDetail.VatTuId,
                y => y.VatTuNewId,
                (x, y) => new { x.PhieuDeXuatDetail, x.PhieuDeXuat, VatTuNew = y })
            .Where(x => x.PhieuDeXuat.TrangThai == (int)SupplyTicketStatus.Unsigned)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == false)
            .Where(x => x.PhieuDeXuatDetail.VatTuId != null && vatTuIds.Contains((int)x.PhieuDeXuatDetail.VatTuId))
            .Select(x => new
            {
                TenVatTu = x.VatTuNew.TenVatTu ?? string.Empty,
                TenPhieu = x.PhieuDeXuat.TenPhieu ?? string.Empty,
                SoLuong = x.PhieuDeXuatDetail.SoLuong ?? 0,
            });
        
        var combinedQuery = listSupplies
                .Union(listSuppliesNew)
                .OrderBy(x => x.TenVatTu);
        return await combinedQuery.ToListAsync();
    } 

}