using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KySo.Response;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.Viettel;
using ESPlatform.QRCode.IMS.Domain.Models.MuaSam;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;


namespace ESPlatform.QRCode.IMS.Core.Services.PhieuKy
{
    public interface IPhieuKyService
    {
        Task<List<PhieuKyModel>> GetDanhSachPhieuKyAsync(DanhSachPhieuKyFilter request);
        Task<int> CapNhatThongTinKyAsync(KyPhieuRequest request);
        Task<object> BoQuaKhongKy(ModifiedKySo request);
        Task<VanBanKyModel> GetVanBanKyById(int id);
        Task <string> GetFullFilePath(string filePath);
        Task<string> GetRelativePath();
        Task<object> UpdateThongTinKyAsync(UpdateFileRequest request);
        Task SignViettelCA(SignMobileCaInputDto request);
        Task<object> UpdateKySimCaAsync(UpdateFileRequest request);
        Task<int> CancelTicketAsync(int phieuId, bool isPhieuDeXuat, string? reason);
        Task<CheckedNumberAndSignImageResponse> CheckedNumberAndSignImageAsync(int phieuId, string accessToken);
    }
}
