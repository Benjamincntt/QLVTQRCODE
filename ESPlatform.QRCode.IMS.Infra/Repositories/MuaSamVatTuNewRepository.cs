using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class MuaSamVatTuNewRepository : EfCoreRepositoryBase<QlvtMuaSamVatTuNew, AppDbContext>,
    IMuaSamVatTuNewRepository
{
    public MuaSamVatTuNewRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<dynamic>> ListAsync(string tenVatTu, string maVatTu, int pageIndex, int pageSize)
    {
        var vatTuNew = DbContext.QlvtMuaSamVatTuNews
        .Where(x => tenVatTu == string.Empty || x.TenVatTu.ToLower().Contains(tenVatTu.ToLower()))
        .Where(x => x.MaVatTu != null && (string.IsNullOrWhiteSpace(maVatTu) || x.MaVatTu.ToLower().Contains(maVatTu)))
        .Select(x => new 
        {
            VatTuId = x.VatTuNewId,
            TenVatTu = x.TenVatTu,
            MaVatTu = x.MaVatTu ?? string.Empty,
            XuatXu = x.XuatXu ?? string.Empty,
            DonViTinh = x.DonViTinh ?? string.Empty,
            Image = x.Image ?? string.Empty,
            IsSystemSupply = false,
            DonGia = x.DonGia ?? 0,
            ThongSoKyThuat = x.ThongSoKyThuat ?? string.Empty,
        })
        .OrderBy(x => x.TenVatTu);
        return await vatTuNew.ToPagedListAsync<dynamic>(pageIndex, pageSize);
    }
}