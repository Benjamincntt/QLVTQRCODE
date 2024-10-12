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
       
        var vatTu = DbContext.QlvtMuaSamPhieuDeXuatDetails
            .GroupJoin(DbContext.QlvtVatTus,
                x => x.VatTuId,
                y => y.VatTuId,
                (x, y) => new { PhieuDeXuatDetail = x, VatTu = y })
            .SelectMany(x => x.VatTu.DefaultIfEmpty(),
                (x, y) => new { x.PhieuDeXuatDetail, VatTu = y })
            .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == true)
            .Select(x => new
            {
                x.PhieuDeXuatDetail.TenVatTu,
                x.PhieuDeXuatDetail.ThongSoKyThuat,
                x.PhieuDeXuatDetail.GhiChu,
                x.PhieuDeXuatDetail.SoLuong,
                Image = x.VatTu == null ? string.Empty : x.VatTu.Image ?? string.Empty,
                DonGia = x.VatTu == null? 0 : x.VatTu.DonGia ?? 0
            });
        //return await vatTu.ToListAsync();
        // lấy thông tin từ bảng vật tư mới tạo
         var vatTuNew = DbContext.QlvtMuaSamPhieuDeXuatDetails
             .GroupJoin(DbContext.QlvtMuaSamVatTuNews,
                 x => x.VatTuId,
                 y => y.VatTuNewId,
                 (x, y) => new { PhieuDeXuatDetail = x, VatTuNew = y })
             .SelectMany(x => x.VatTuNew.DefaultIfEmpty(),
                 (x, y) => new { x.PhieuDeXuatDetail, VatTuNew = y })
             .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
             .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == false)
             .Select(x => new
             {
                 x.PhieuDeXuatDetail.TenVatTu,
                 x.PhieuDeXuatDetail.ThongSoKyThuat,
                 x.PhieuDeXuatDetail.GhiChu,
                 x.PhieuDeXuatDetail.SoLuong,
                 Image = x.VatTuNew == null ? string.Empty : x.VatTuNew.Image ?? string.Empty,
                 DonGia = x.VatTuNew == null? 0 : x.VatTuNew.DonGia ?? 0
             });
         var combinedQuery = vatTu
                 .Union(vatTuNew)
                 .OrderBy(x => x.TenVatTu)
             ;
        return await combinedQuery.ToListAsync();
        
    }
}