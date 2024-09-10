using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;

namespace ESPlatform.QRCode.IMS.Core.Services.MuaSamVatTu;

public class MuaSamVatTuService : IMuaSamVatTuService
{
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;

    public MuaSamVatTuService(
        IVatTuRepository vatTuRepository,
        IAuthorizedContextFacade authorizedContextFacade)
    {
        _vatTuRepository = vatTuRepository;
        _authorizedContextFacade = authorizedContextFacade;
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
        return response;
    }

    public async Task<PurchaseSupplyResponse> GetPurchaseSupplyAsync(int vatTuId)
    {
        #region validate
        if (vatTuId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidId);
        }
        var vatTu = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId);
        if (vatTu == null)
        {
            throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
        }
        #endregion
        
        var response = new PurchaseSupplyResponse();
        response.TenVatTu = vatTu.TenVatTu;
        response.MoTa = vatTu.MoTa ?? string.Empty;
        var folderPath = $@"{AppConfig.Instance.Image.FolderPath}\{vatTuId}";
        var urlPath = $"{AppConfig.Instance.Image.UrlPath}/{vatTuId}";
        if (Directory.Exists(folderPath))
        {
            var imageFiles = Directory.GetFiles(folderPath);
            
            foreach (var file in imageFiles)
            {
                var fileName = Path.GetFileName(file);
                var fullPath = $"{urlPath}/{fileName}";
                response.ImagePaths.Add(fullPath);
            }
        }
        return response;
    }

    public async Task<int> CreateSupplyAsync(CreatedSupplyRequest request)
    {   
        #region validate

        await ValidationHelper.ValidateAsync(request, new CreatedSupplyRequestValidation());
        var existSupplyName = await _vatTuRepository.ExistsAsync(x => x.TenVatTu == request.TenVatTu);
        if (existSupplyName)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.ExistedSupplyName);
        }
        var existSupplyCode = await _vatTuRepository.ExistsAsync(x => x.MaVatTu == request.MaVatTu);
        if (existSupplyCode)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.ExistedSupplyCode);
        }
        
        #endregion 
        var vatTu = request.Adapt<QlvtVatTu>();
        vatTu.NguoiTaoId = _authorizedContextFacade.AccountId;
        vatTu.NguoiTao = _authorizedContextFacade.Username;
        vatTu.NgayTao = DateTime.Now;
        return await _vatTuRepository.InsertAsync(vatTu);
    }
    
    
}