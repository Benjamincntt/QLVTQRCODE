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

    public async Task<IEnumerable<dynamic>> ListSupplyAsync(int userId, string relativeBasePath)
    {
        var vatTu = DbContext.QlvtGioHangs
            .Join(DbContext.QlvtVatTus,
                x => x.VatTuId,
                y => y.VatTuId,
                (x, y) => new { QlvtGioHang = x, VatTu = y })
            .Where(x => x.QlvtGioHang.UserId == userId)
            .Where(x => x.QlvtGioHang.IsSystemSupply == true)
            .Select(x => new
            {
                x.VatTu.TenVatTu,
                x.VatTu.DonGia,
                Image = x.VatTu.Image != null ? (relativeBasePath + x.VatTu.Image) : null,
                x.QlvtGioHang.VatTuId,
                x.QlvtGioHang.IsSystemSupply,
                ThongSoKyThuat = x.VatTu.MoTa,
                x.QlvtGioHang.GhiChu,
                x.QlvtGioHang.SoLuong,
                x.QlvtGioHang.GioHangId,
            });
        //return await vatTu.ToListAsync();
        // lấy thông tin từ bảng vật tư mới tạo
        var vatTuNew = DbContext.QlvtGioHangs
            .Join(DbContext.QlvtMuaSamVatTuNews,
                x => x.VatTuId,
                y => y.VatTuNewId,
                (x, y) => new { QlvtGioHang = x, VatTuNew = y })
            .Where(x => x.QlvtGioHang.UserId == userId)
            .Where(x => x.QlvtGioHang.IsSystemSupply == false)
            .Select(x => new
            {
                x.VatTuNew.TenVatTu,
                x.VatTuNew.DonGia,
                Image = x.VatTuNew.Image ?? string.Empty,
                x.QlvtGioHang.VatTuId,
                x.QlvtGioHang.IsSystemSupply,
                x.QlvtGioHang.ThongSoKyThuat,
                x.QlvtGioHang.GhiChu,
                x.QlvtGioHang.SoLuong,
                x.QlvtGioHang.GioHangId
            });
        var combinedQuery = vatTu
                .Union(vatTuNew)
                .OrderBy(x => x.TenVatTu)
            ;
        return await combinedQuery.ToListAsync();
        
    }
}