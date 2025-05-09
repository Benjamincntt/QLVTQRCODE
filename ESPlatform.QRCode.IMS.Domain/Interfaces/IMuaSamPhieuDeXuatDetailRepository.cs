﻿using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamPhieuDeXuatDetailRepository : IRepositoryBase<QlvtMuaSamPhieuDeXuatDetail>
{
    Task<IEnumerable<dynamic>> ListAsync(int supplyTicketId);
}