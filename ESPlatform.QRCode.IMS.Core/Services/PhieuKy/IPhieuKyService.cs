using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESPlatform.QRCode.IMS.Core.Services.PhieuKy
{
    public interface IPhieuKyService
    {
        Task<List<PhieuKyModel>> GetDanhSachPhieuKyAsync(DanhSachPhieuKyFilter request);
        Task<int> CapNhatThongTinKyAsync(KyPhieuRequest request);
        Task<int> BoQuaKhongKy(ModifiedKySo request);
        Task<VanBanKyModel> GetVanBanKyById(int id);
        Task <string> GetFullFilePath(string filePath);
        Task<string> GetRelativePath();
        Task<object> UpdateThongTinKyAsync(UpdateFileRequest request);
    }
}
