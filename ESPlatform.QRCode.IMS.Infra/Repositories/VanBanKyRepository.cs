﻿using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class VanBanKyRepository : EfCoreRepositoryBase<QlvtVanBanKy, AppDbContext>, IVanBanKyRepository
{
    public VanBanKyRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<string?>> ListVanbanKyUrlAsync(int phieuId)
    {
        var query = DbContext.QlvtVanBanKies
            .Where(x => x.PhieuId == phieuId)
            .Where(x => x.FilePath != null)
            .Select(x => x.FilePath);
        return await query.ToListAsync();
    }
}