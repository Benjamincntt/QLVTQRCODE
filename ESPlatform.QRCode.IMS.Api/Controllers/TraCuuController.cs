using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.Lookup;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class TraCuuController: ApiControllerBase
{
    private readonly ILookupService _lookupService;

    public TraCuuController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    /// <summary>
    /// Tra cứu vật tư
    /// </summary>
    /// <param name="maVatTu"></param>
    /// <returns></returns>
    [HttpGet("{maVatTu}")]
    public async Task<LookupSuppliesResponse> GetAsync(string maVatTu)
    {
        return await _lookupService.GetAsync(maVatTu);
    }
}