using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class VatTuRepository : EfCoreRepositoryBase<QlvtVatTu, AppDbContext>, IVatTuRepository
{
    public VatTuRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<dynamic?> GetInventoryCheckInformationAsync(int vatTuId, int kykiemkeId)
    {
        var response = await DbContext.QlvtKyKiemKeChiTiets
            .Join(DbContext.QlvtKyKiemKes,
                x => x.KyKiemKeId,
                y => y.Id,
                (x, y) => new { QlvtKyKiemKeChiTiet = x, QlvtKyKiemKe = y })
            .Where(x => x.QlvtKyKiemKeChiTiet.VatTuId == vatTuId)
            .Where(x => x.QlvtKyKiemKeChiTiet.KyKiemKeId == kykiemkeId)
            .Select(x => new
            {
                x.QlvtKyKiemKeChiTiet.KyKiemKeChiTietId,
                x.QlvtKyKiemKe.PhysicalInventoryName, // ten ky kiem ke
                x.QlvtKyKiemKeChiTiet.SoLuongSoSach,
                x.QlvtKyKiemKeChiTiet.SoLuongKiemKe,
                x.QlvtKyKiemKeChiTiet.SoLuongChenhLech,
                x.QlvtKyKiemKeChiTiet.SoThe,
                x.QlvtKyKiemKeChiTiet.TrangThai,
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<IEnumerable<dynamic>> GetPositionAsync(int vatTuId)
    {
        var response = await DbContext.QlvtVatTuViTris
            .Where(x => x.IdVatTu == vatTuId)
            .Select(x => new
            {
                x.IdViTri,
                x.IdToMay,
                x.IdGiaKe,
                x.IdNgan,
                x.ViTri
            })
            .ToListAsync();
        return response;
    }

    public async Task<dynamic?> GetLotNumberAsync(int vatTuId, int khoId)
    {
        var response = await DbContext.QlvtVatTuTonKhos
            .Where(x => x.InventoryItemId == vatTuId)
            .Where(x => x.OrganizationId == khoId)
            .Select(x => new
            {
                LotNumber = x.LotNumber ?? string.Empty,
                OnhandQuantity = x.OnhandQuantity ?? 0
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<PagedList<dynamic>> ListAsync(
        string tenVatTu,
        string maVatTu,
        int idKho,
        List<int>? listIdToMay,
        List<int>? listIdGiaKe,
        List<int>? listIdNgan,
        List<string>? listMaNhom,
        List<int>? listVatTuTonKhoIds,
        string relativeBasePath,
        int pageIndex,
        int pageSize)
    {
        var vatTu = DbContext.QlvtVatTus
            .GroupJoin(DbContext.QlvtVatTuViTris,
                x => x.VatTuId,
                y => y.IdVatTu,
                (x, y) => new { QlvtVatTu = x, QlvtVatTuViTri = y })
            .SelectMany(x => x.QlvtVatTuViTri.DefaultIfEmpty(),
                (x, y) => new { x.QlvtVatTu, QlvtVatTuViTri = y })
            .Where(x => string.IsNullOrWhiteSpace(tenVatTu) || x.QlvtVatTu.TenVatTu != null && x.QlvtVatTu.TenVatTu.ToLower().Contains(tenVatTu))
            .Where(x => string.IsNullOrWhiteSpace(maVatTu) || x.QlvtVatTu.MaVatTu != null && x.QlvtVatTu.MaVatTu.ToLower().Contains(maVatTu))
            .Where(x => x.QlvtVatTu.KhoId == idKho)
            .Where(x => listMaNhom == null || !listMaNhom.Any() || x.QlvtVatTu.MaVatTu != null && listMaNhom.Contains(x.QlvtVatTu.MaVatTu.Substring(0,7)))
            .Where(x => listIdToMay == null || !listIdToMay.Any() || x.QlvtVatTuViTri != null && listIdToMay.Contains((int)x.QlvtVatTuViTri.IdToMay!))
            .Where(x => listIdGiaKe == null ||!listIdGiaKe.Any() || x.QlvtVatTuViTri != null && listIdGiaKe.Contains((int)x.QlvtVatTuViTri.IdGiaKe!))
            .Where(x => listIdNgan == null ||!listIdNgan.Any() || x.QlvtVatTuViTri != null && listIdNgan.Contains((int)x.QlvtVatTuViTri.IdNgan!))
            .Where(x => listVatTuTonKhoIds == null || !listVatTuTonKhoIds.Any() || !listVatTuTonKhoIds.Contains(x.QlvtVatTu.VatTuId))
            .OrderBy(x => x.QlvtVatTu.MaVatTu)
            .ThenBy(x => x.QlvtVatTu.TenVatTu)
            .Select(x => new 
            {
                VatTuId = x.QlvtVatTu.VatTuId,
                TenVatTu = x.QlvtVatTu.TenVatTu ?? string.Empty,
                MaVatTu = x.QlvtVatTu.MaVatTu ?? string.Empty,
                DonViTinh = x.QlvtVatTu.DonViTinh ?? string.Empty,
                XuatXu = string.Empty,
                Image = x.QlvtVatTu.Image != null ? (relativeBasePath + x.QlvtVatTu.Image) : string.Empty,
                IsSystemSupply = true,
                DonGia = x.QlvtVatTu.DonGia ?? 0,
                ThongSoKyThuat = x.QlvtVatTu.MoTa ?? string.Empty,
                OnhandQuantity = 0
            });

        return await vatTu.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }

    public async Task<dynamic?> GetWarehouseIdAsync(int vatTuId)
    {
        var query = DbContext.QlvtKhos
            .Join(DbContext.QlvtVatTus,
                x => x.OrganizationId,
                y => y.KhoId,
                (x, y) => new { QlvtKho = x, QlvtVatTu = y })
            .GroupJoin(DbContext.QlvtKhos,
                x => x.QlvtKho.SubInventoryCode,
                y => y.OrganizationCode,
                (x, y) => new { x.QlvtKho, x.QlvtVatTu, KhoPhu = y })
            .SelectMany(x => x.KhoPhu.DefaultIfEmpty(),
                (x, y) => new
                {
                    x.QlvtKho,
                    x.QlvtVatTu,
                    KhoPhu = y
                })
            .Where(x => x.QlvtVatTu.VatTuId == vatTuId)
            .Select(x => new
            {
                KhoChinhId = x.QlvtKho.OrganizationId,
                KhoPhuId = x.KhoPhu != null ? x.KhoPhu.OrganizationId : 0
            });
        return await query.FirstOrDefaultAsync();
    }
}