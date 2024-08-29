using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class VatTuImageRepository: EfCoreRepositoryBase<QlvtVatTuImage, AppDbContext>, IVatTuImageRepository
{
    public VatTuImageRepository(AppDbContext dbContext) : base(dbContext) { }
}