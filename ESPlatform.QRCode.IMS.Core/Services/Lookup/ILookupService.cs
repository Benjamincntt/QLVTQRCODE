using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;

namespace ESPlatform.QRCode.IMS.Core.Services.Lookup;

public interface ILookupService
{
    Task<LookupSuppliesResponse> GetAsync(string maVatTu);
}