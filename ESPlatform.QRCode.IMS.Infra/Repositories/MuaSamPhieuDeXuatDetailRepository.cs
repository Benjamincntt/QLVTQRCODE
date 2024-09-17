using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class MuaSamPhieuDeXuatDetailRepository : EfCoreRepositoryBase<QlvtMuaSamPhieuDeXuatDetail, AppDbContext>, IMuaSamPhieuDeXuatDetailRepository
{
    public MuaSamPhieuDeXuatDetailRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<dynamic>> ListAsync(int supplyTicketId)
    {
        // lấy thêm image từ bảng vật tư
        var vatTu = DbContext.QlvtMuaSamPhieuDeXuatDetails
            .Join(DbContext.QlvtVatTus,
                x => x.IdVatTu,
                y => y.VatTuId,
                (x, y) => new { PhieuDeXuatDetail = x, VatTu = y })
            .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == true)
            .OrderBy(x => x.PhieuDeXuatDetail.TenVatTu)
            .Select(x => new
            {
                x.PhieuDeXuatDetail.TenVatTu,
                x.PhieuDeXuatDetail.ThongSoKyThuat,
                x.PhieuDeXuatDetail.GhiChu,
                x.PhieuDeXuatDetail.SoLuong,
                x.VatTu.Image
            });
        // lấy thông tin từ bảng vật tư mới tạo
        var vatTuNew = DbContext.QlvtMuaSamPhieuDeXuatDetails
            .Join(DbContext.QlvtMuaSamVatTuNews,
                x => x.IdVatTu,
                y => y.VatTuNewId,
                (x, y) => new { PhieuDeXuatDetail = x, VatTuNew = y })
            .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == false)
            .OrderBy(x => x.PhieuDeXuatDetail.TenVatTu)
            .Select(x => new
            {
                x.PhieuDeXuatDetail.TenVatTu,
                x.PhieuDeXuatDetail.ThongSoKyThuat,
                x.PhieuDeXuatDetail.GhiChu,
                x.PhieuDeXuatDetail.SoLuong,
                x.VatTuNew.Image
            });
        var combinedQuery = vatTu
                .Union(vatTuNew)
                .OrderBy(x => x.TenVatTu)
            ;
        return await combinedQuery.ToListAsync();
    }
}