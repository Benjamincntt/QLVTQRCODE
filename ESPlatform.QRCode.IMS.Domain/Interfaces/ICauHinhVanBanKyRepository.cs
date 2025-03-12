using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces
{
    public interface ICauHinhVanBanKyRepository : IRepositoryBase<QlvtCauHinhVbKy>
    {
        Task<QlvtCauHinhVbKy?> GetCauHinhVbKyAsync(string maDoiTuongKyHienTai);
    }
}
