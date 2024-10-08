using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces
{
    public interface IPhieuKyRepository: IRepositoryBase<QlvtMuaSamPhieuDeXuat>
    {
        Task<IEnumerable<dynamic>> DanhSachPhieuDeXuatKy(Library.Utils.Filters.DanhSachPhieuKyFilter requests, int userId);
    }
}
