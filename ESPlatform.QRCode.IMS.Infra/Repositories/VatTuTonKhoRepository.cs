using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class VatTuTonKhoRepository : EfCoreRepositoryBase<QlvtVatTuTonKho, AppDbContext>, IVatTuTonKhoRepository
{
    public VatTuTonKhoRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<int>> ListVatTuIdAsync()
    {
        return await DbContext.QlvtVatTuTonKhos
            .Select(x => x.VatTuId)
            .Distinct()
            .ToListAsync();
    }

    public async Task<PagedList<dynamic>> ListAsync(
        string tenVatTu,
        string maVatTu,
        int idKho,
        List<int>? listIdToMay,
        List<int>? listIdGiaKe, 
        List<int>? listIdNgan,
        List<string>? listMaNhom,
        int pageIndex,
        int pageSize)
    {
        var response = DbContext.QlvtVatTuTonKhos
            .Join(DbContext.QlvtVatTus,
                x => x.VatTuId,
                y => y.VatTuId,
                (x, y) => new { QlvtVatTuTonKho = x, QlvtVatTu = y })
            .GroupJoin(DbContext.QlvtVatTuViTris,
                x => x.QlvtVatTu.VatTuId,
                y => y.IdVatTu,
                (x, y) => new { x.QlvtVatTuTonKho, x.QlvtVatTu, QlvtVatTuViTri = y })
            .SelectMany(x => x.QlvtVatTuViTri.DefaultIfEmpty(),
                (x, y) => new { x.QlvtVatTuTonKho, x.QlvtVatTu, QlvtVatTuViTri = y })
            .Where(x => string.IsNullOrWhiteSpace(tenVatTu) ||
                        x.QlvtVatTu.TenVatTu != null && x.QlvtVatTu.TenVatTu.ToLower().Contains(tenVatTu))
            .Where(x => string.IsNullOrWhiteSpace(maVatTu) ||
                        x.QlvtVatTu.MaVatTu != null && x.QlvtVatTu.MaVatTu.ToLower().Contains(maVatTu))
            .Where(x => x.QlvtVatTu.KhoId == idKho)
            .Where(x => listMaNhom == null || !listMaNhom.Any() || x.QlvtVatTu.MaVatTu != null &&
                listMaNhom.Contains(x.QlvtVatTu.MaVatTu.Substring(0, 7)))
            .Where(x => listIdToMay == null || !listIdToMay.Any() ||
                        x.QlvtVatTuViTri != null && listIdToMay.Contains((int)x.QlvtVatTuViTri.IdToMay!))
            .Where(x => listIdGiaKe == null || !listIdGiaKe.Any() ||
                        x.QlvtVatTuViTri != null && listIdGiaKe.Contains((int)x.QlvtVatTuViTri.IdGiaKe!))
            .Where(x => listIdNgan == null || !listIdNgan.Any() ||
                        x.QlvtVatTuViTri != null && listIdNgan.Contains((int)x.QlvtVatTuViTri.IdNgan!))
            .Where(x => x.QlvtVatTuTonKho.KhoId == idKho)
            .OrderBy(x => x.QlvtVatTuTonKho.MaVatTu)
            .ThenBy(x => x.QlvtVatTuTonKho.TenVatTu)
            .Select(x => new
            { 
                x.QlvtVatTuTonKho.VatTuId,
                TenVatTu = x.QlvtVatTuTonKho.TenVatTu ?? string.Empty,
                MaVatTu = x.QlvtVatTuTonKho.MaVatTu ?? string.Empty,
                DonViTinh = x.QlvtVatTuTonKho.DonViTinh ?? string.Empty,
                XuatXu = string.Empty,
                Image = string.Empty,
                IsSystemSupply = true,
                DonGia = x.QlvtVatTu.DonGia ?? 0,
                ThongSoKyThuat = x.QlvtVatTu.MoTa ?? string.Empty,
                OnhandQuantity = x.QlvtVatTuTonKho.OnhandQuantity ?? 0,
            });
        return await response.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }
}