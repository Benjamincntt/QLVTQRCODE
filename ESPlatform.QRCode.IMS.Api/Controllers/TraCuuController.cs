using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;
using ESPlatform.QRCode.IMS.Core.Services.TraCuu;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class TraCuuController: ApiControllerBase
{
    private readonly ITraCuuService _traCuuService;

    public TraCuuController(ITraCuuService traCuuService)
    {
        _traCuuService = traCuuService;
    }

    /// <summary>
    /// Tra cứu vật tư
    /// </summary>
    /// <param name="khoId"></param>
    /// <param name="maVatTu"></param>
    /// <returns></returns>
    [HttpGet("{khoId:int}/{maVatTu}")]
    public async Task<LookupSuppliesResponse> GetAsync(int khoId, string maVatTu)
    {
        return await _traCuuService.GetAsync(khoId, maVatTu);
    }
}