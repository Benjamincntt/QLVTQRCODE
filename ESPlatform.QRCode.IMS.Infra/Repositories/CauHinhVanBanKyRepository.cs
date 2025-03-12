using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;


namespace ESPlatform.QRCode.IMS.Infra.Repositories
{
    public class CauHinhVanBanKyRepository: EfCoreRepositoryBase<QlvtCauHinhVbKy, AppDbContext>, ICauHinhVanBanKyRepository
    {
        public CauHinhVanBanKyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<QlvtCauHinhVbKy?> GetCauHinhVbKyAsync(string maDoiTuongKyHienTai)
        {
            var query = DbContext.QlvtCauHinhVbKies
                .Where(x => x.MaDoiTuongKy == maDoiTuongKyHienTai.Trim());

            return await query.FirstOrDefaultAsync(); 
        }
    }
}
