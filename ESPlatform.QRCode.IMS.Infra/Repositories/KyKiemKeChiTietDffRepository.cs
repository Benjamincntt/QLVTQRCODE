using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class KyKiemKeChiTietDffRepository : EfCoreRepositoryBase<QlvtKyKiemKeChiTietDff, AppDbContext>,
    IKyKiemKeChiTietDffRepository
{
    public KyKiemKeChiTietDffRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}