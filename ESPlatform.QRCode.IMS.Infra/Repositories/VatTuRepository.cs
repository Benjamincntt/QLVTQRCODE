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
                x.IdHop,
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
                x.LotNumber
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<PagedList<dynamic>> ListAsync(string tenVatTu, string maVatTu, int idKho, int idViTri, string maNhom, string relativeBasePath,
        int pageIndex, int pageSize)
    {
        var vatTu = DbContext.QlvtVatTus
            .GroupJoin(DbContext.QlvtKhos,
                x => x.KhoId,
                y => y.OrganizationId,
                (x, y) => new { QlvtVatTu = x, QlvtKho = y })
            .SelectMany(x => x.QlvtKho.DefaultIfEmpty(),
                (x, y) => new { x.QlvtVatTu, QlvtKho = y })
            .GroupJoin(DbContext.QlvtVatTuViTris,
                x => x.QlvtVatTu.VatTuId,
                y => y.IdVatTu,
                (x, y) => new { x.QlvtVatTu, x.QlvtKho, QlvtVatTuViTri = y })
            .SelectMany(x => x.QlvtVatTuViTri.DefaultIfEmpty(),
                (x, y) => new { x.QlvtVatTu, x.QlvtKho, QlvtVatTuViTri = y })
            .Where(x => string.IsNullOrWhiteSpace(tenVatTu) || x.QlvtVatTu.TenVatTu.ToLower().Contains(tenVatTu))
            .Where(x => string.IsNullOrWhiteSpace(maVatTu) || x.QlvtVatTu.MaVatTu.ToLower().Contains(maVatTu))
            .Where(x => string.IsNullOrWhiteSpace(maNhom) || x.QlvtVatTu.MaVatTu.ToLower().Contains(maNhom))
            .Where(x => idKho == 0 || x.QlvtVatTu.KhoId == idKho)
            .Where(x => idViTri == 0 || x.QlvtVatTuViTri != null &&
                (x.QlvtVatTuViTri.IdToMay == idViTri
                 || x.QlvtVatTuViTri.IdGiaKe == idViTri
                 || x.QlvtVatTuViTri.IdNgan == idViTri
                 || x.QlvtVatTuViTri.IdHop == idViTri))
            .OrderBy(x => x.QlvtVatTu.TenVatTu)
            .Select(x => new 
            {
                VatTuId = x.QlvtVatTu.VatTuId,
                TenVatTu = x.QlvtVatTu.TenVatTu,
                DonViTinh = x.QlvtVatTu.DonViTinh,
                Image = x.QlvtVatTu.Image != null ? (relativeBasePath + x.QlvtVatTu.Image) : string.Empty,
                IsSystemSupply = true,
            });
        // Query cho dữ liệu mới
        // var vatTuNew = DbContext.QlvtMuaSamVatTuNews
        //     .Where(x => tenVatTu == string.Empty || x.TenVatTu.ToLower().Contains(tenVatTu.ToLower()))
        //     .Where(x => maVatTu == string.Empty)
        //     .Where(x => idKho == 0)
        //     .Where(x => idViTri == 0)
        //     .Select(x => new 
        //     {
        //         VatTuId = x.VatTuNewId,
        //         TenVatTu = x.TenVatTu,
        //         DonViTinh = x.DonViTinh,
        //         Image = x.Image ?? string.Empty,
        //         IsSystemSupply = false,
        //     });
        //
        // // Kết hợp cả hai query
        // var combinedQuery = vatTu
        //     .Union(vatTuNew)
        //     .OrderBy(x => x.TenVatTu)
        //     ;

        // Phân trang kết quả kết hợp
        // return await combinedQuery.ToPagedListAsync<dynamic>(pageIndex, pageSize);
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