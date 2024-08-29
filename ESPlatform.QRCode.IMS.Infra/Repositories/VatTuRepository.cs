using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
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
            .GroupJoin(DbContext.QlvtKhos,
                x => x.QlvtKyKiemKeChiTiet.KhoChinhId,
                y => y.OrganizationId,
                (x, y) => new { x.QlvtKyKiemKeChiTiet, x.QlvtKyKiemKe, QlvtKho = y })
            .SelectMany(x => x.QlvtKho.DefaultIfEmpty(),
                (x, y) => new { x.QlvtKyKiemKeChiTiet, x.QlvtKyKiemKe, QlvtKho = y })
            .Where(x => x.QlvtKyKiemKeChiTiet.VatTuId == vatTuId)
            .Where(x => x.QlvtKyKiemKeChiTiet.KyKiemKeId == kykiemkeId)
            .Select(x => new 
            {
                x.QlvtKyKiemKe.PhysicalInventoryName,// ten ky kiem ke
                x.QlvtKyKiemKe.OrganizationCode,// mã kho,
                SubInventoryCode = x.QlvtKho != null ? x.QlvtKho.SubInventoryCode : string.Empty,
                SubInventoryName =  x.QlvtKho != null ? x.QlvtKho.SubInventoryName : string.Empty,
                x.QlvtKyKiemKeChiTiet.SoLuongSoSach,
                x.QlvtKyKiemKeChiTiet.SoLuongKiemKe,
                x.QlvtKyKiemKeChiTiet.SoLuongChenhLech
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<dynamic?> GetPositionAsync(int vatTuId)
    {
        var response = await DbContext.QlvtVatTuViTris
            .Where(x => x.IdVatTu == vatTuId)
            .Select(x => new 
            {
                x.IdToMay,
                x.IdGiaKe,
                x.IdNgan,
                x.IdHop,
                x.ViTri
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<dynamic?> GetWareHouseAsync(int vatTuId)
    {
        var response = await DbContext.QlvtVatTuKhos
            .Join(DbContext.QlvtKhos,
                x => x.OrganizationId,
                y => y.OrganizationId,
                (x, y) => new { QlvtVatTuKho = x, QlvtKho = y })

            .Where(x => x.QlvtVatTuKho.InventoryItemId == vatTuId)
            
            .Select(x => new 
            {
                x.QlvtVatTuKho.LotNumber
            })
            .FirstOrDefaultAsync();
        return response;
    }

    public async Task<IEnumerable<dynamic>> ListAsync(int vatTuId)
    {
        var response = await DbContext.QlvtVatTuImages
            .Join(DbContext.QlvtAttachments,
                x => x.AttachmentId,
                y => y.Id,
                (x, y) => new { QlvtVatTuImage = x, QlvtAttachment = y })

            .Where(x => x.QlvtVatTuImage.IdVatTu == vatTuId)
            .Select(x => new 
            {
                x.QlvtAttachment.AttachmentUrl,
                x.QlvtAttachment.Id
            })
            .ToListAsync();
        return response;
    }
}