using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IVatTuTonKhoRepository : IRepositoryBase<QlvtVatTuTonKho>
{
    // Task<PagedList<dynamic>> ListAsync(int idKho, string relativeBasePath, int pageIndex,
    //     int pageSize);

    Task<IEnumerable<int>> ListVatTuIdAsync();

    Task<PagedList<dynamic>> ListAsync(
        string tenVatTu,
        string maVatTu,
        int idKho,
        List<int>? listIdToMay,
        List<int>? listIdGiaKe,
        List<int>? listIdNgan,
        List<string>? listMaNhom,
        string relativeBasePath,
        int getPageIndex,
        int getPageSize);
}