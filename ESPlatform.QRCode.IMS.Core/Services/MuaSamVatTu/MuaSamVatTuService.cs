using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public class MuaSamVatTuService : IMuaSamVatTuService
{
    private readonly IVatTuRepository _vatTuRepository;

    public MuaSamVatTuService(IVatTuRepository vatTuRepository)
    {
        _vatTuRepository = vatTuRepository;
    }

    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request)
    {
        await ValidationHelper.ValidateAsync(request, new SupplyListRequestValidation());
        var response = (await _vatTuRepository.ListAsync(
                string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                request.IdKho,
                request.IdViTri,
                request.GetPageIndex(),
                request.GetPageSize()))
            .Adapt<PagedList<SupplyListResponseItem>>();
        // if (response )
        // {
        //     
        // }
        return response;
    }
}