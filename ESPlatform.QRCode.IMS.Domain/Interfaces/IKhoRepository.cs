﻿using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IKhoRepository : IRepositoryBase<QlvtKho>
{
    Task<IEnumerable<dynamic>> ListWarehousesAsync();
}