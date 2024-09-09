using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class KyKiemKeChiTietRepository : EfCoreRepositoryBase<QlvtKyKiemKeChiTiet, AppDbContext>, IKyKiemKeChiTietRepository
{
    public KyKiemKeChiTietRepository(AppDbContext dbContext) : base(dbContext) {}
}