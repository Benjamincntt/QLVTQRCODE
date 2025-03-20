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
            .Join(DbContext.QlvtVatTus,
                x => x.VatTuId,
                y => y.VatTuId,
                (x, y) => new { PhieuDeXuatDetail = x, VatTu = y })
            .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
            .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == true)
            .Select(x => new
            {
                VatTuId = x.VatTu.VatTuId,
                IsSystemSupply = true,
                TenVatTu = x.VatTu.TenVatTu ?? string.Empty,
                ThongSoKyThuat = x.PhieuDeXuatDetail.ThongSoKyThuat ?? string.Empty,
                GhiChu = x.PhieuDeXuatDetail.GhiChu ?? string.Empty,
                SoLuong = x.PhieuDeXuatDetail.SoLuong ?? 0,
                DonGia = x.VatTu.DonGia ?? 0,
                PhieuDeXuatDetailId = x.PhieuDeXuatDetail.Id,
            });
        // lấy thông tin từ bảng vật tư mới tạo
         var vatTuNew = DbContext.QlvtMuaSamPhieuDeXuatDetails
             .Join(DbContext.QlvtMuaSamVatTuNews,
                 x => x.VatTuId,
                 y => y.VatTuNewId,
                 (x, y) => new { PhieuDeXuatDetail = x, VatTuNew = y })
             .Where(x => x.PhieuDeXuatDetail.PhieuDeXuatId == supplyTicketId)
             .Where(x => x.PhieuDeXuatDetail.IsSystemSupply == false)
             .Select(x => new
             {
                 VatTuId = x.VatTuNew.VatTuNewId,
                 IsSystemSupply = false,
                 TenVatTu = x.VatTuNew.TenVatTu ?? string.Empty,
                 ThongSoKyThuat = x.PhieuDeXuatDetail.ThongSoKyThuat ?? string.Empty,
                 GhiChu = x.PhieuDeXuatDetail.GhiChu ?? string.Empty,
                 SoLuong = x.PhieuDeXuatDetail.SoLuong ?? 0,
                 DonGia = x.VatTuNew.DonGia ?? 0,
                 PhieuDeXuatDetailId = x.PhieuDeXuatDetail.Id,
             });
         var combinedQuery = vatTu
                 .Union(vatTuNew)
                 .OrderBy(x => x.TenVatTu)
             ;
        return await combinedQuery.ToListAsync();
        
    }
}