using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Infra.Repositories
{
    public class PhieuDeXuatKyRepository: EfCoreRepositoryBase<QlvtMuaSamPdxKy, AppDbContext>, IPhieuDeXuatKyRepository
    {
        public PhieuDeXuatKyRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
