using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;

namespace ESPlatform.QRCode.IMS.Core.Services.TraCuu;

public interface ITraCuuService
{
    Task<LookupSuppliesResponse> GetAsync(string maVatTu);
}