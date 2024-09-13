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
                x.QlvtKyKiemKeChiTiet.SoLuongChenhLech
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

    public async Task<dynamic?> GetInventoryAsync(int vatTuId, int khoId)
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

    public async Task<PagedList<dynamic>> ListAsync(string tenVatTu, string maVatTu, int idKho, int idViTri,
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
            .Where(x => tenVatTu == string.Empty || x.QlvtVatTu.TenVatTu.ToLower().Contains(tenVatTu))
            .Where(x => maVatTu == string.Empty || x.QlvtVatTu.MaVatTu.ToLower().Contains(maVatTu))
            .Where(x => idKho == 0 || x.QlvtVatTu.KhoId == idKho)
            .Where(x => idViTri == 0 || x.QlvtVatTuViTri != null &&
                (x.QlvtVatTuViTri.IdToMay == idViTri
                 || x.QlvtVatTuViTri.IdGiaKe == idViTri
                 || x.QlvtVatTuViTri.IdNgan == idViTri
                 || x.QlvtVatTuViTri.IdHop == idViTri))
            .Select(x => new 
            {
                VatTuId = x.QlvtVatTu.VatTuId,
                TenVatTu = x.QlvtVatTu.TenVatTu,
                DonViTinh = x.QlvtVatTu.DonViTinh,
                IsVatTu = true,
            });
        // Query cho dữ liệu mới
        var vatTuNew = DbContext.QlvtMuaSamVatTuNews
            .Where(x => tenVatTu == string.Empty || x.TenVatTu.ToLower().Contains(tenVatTu.ToLower()))
            .Where(x => maVatTu == string.Empty)
            .Where(x => idKho == 0)
            .Where(x => idViTri == 0)
            .Select(x => new 
            {
                VatTuId = x.VatTuNewId,
                TenVatTu = x.TenVatTu,
                DonViTinh = x.DonViTinh,
                IsVatTu = false,
            });

        // Kết hợp cả hai query
        var combinedQuery = vatTu
            .Union(vatTuNew)
            .OrderBy(x => x.TenVatTu)
            ;

        // Phân trang kết quả kết hợp
        return await combinedQuery.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }
}