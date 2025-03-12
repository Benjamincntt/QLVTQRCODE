using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IMuaSamPdxKyRepository : IRepositoryBase<QlvtMuaSamPdxKy>
{
    Task<IEnumerable<int>> ListPhieuDeXuatIdsAsync(string maDoiTuongKy);
    Task<IEnumerable<int>> ListPhieuDeXuatOtherIdsAsync(string previousMaDoiTuongKy, List<int> listPhieuDeXuatIds);
}