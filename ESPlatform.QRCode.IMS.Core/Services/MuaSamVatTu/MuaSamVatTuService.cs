using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
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
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IMuaSamPhieuDeXuatRepository _muaSamPhieuDeXuatRepository;
    private readonly IMuaSamPhieuDeXuatDetailRepository _muaSamPhieuDeXuatDetailRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;

    public MuaSamVatTuService(
        IVatTuRepository vatTuRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMuaSamPhieuDeXuatRepository muaSamPhieuDeXuatRepository,
        IMuaSamPhieuDeXuatDetailRepository muaSamPhieuDeXuatDetailRepository,
        IAuthorizedContextFacade authorizedContextFacade)
    {
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _muaSamPhieuDeXuatRepository = muaSamPhieuDeXuatRepository;
        _muaSamPhieuDeXuatDetailRepository = muaSamPhieuDeXuatDetailRepository;
        _authorizedContextFacade = authorizedContextFacade;
    }

    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request)
    {
        await ValidationHelper.ValidateAsync(request, new SupplyListRequestValidation());
        var listVatTu = (await _vatTuRepository.ListAsync(
                string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                request.IdKho,
                request.IdViTri,
                request.GetPageIndex(),
                request.GetPageSize()))
            .Adapt<PagedList<SupplyListResponseItem>>();
        return listVatTu;
    }

    public async Task<PurchasedSupplyResponse> GetPurchaseSupplyAsync(int vatTuId, bool isSystemSupply)
    {
        if (vatTuId <= 0 )
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidId);
        }
        
        var response = new PurchasedSupplyResponse();
        // if Id is VatTuId => get information from QlvtVatTu table
        if (isSystemSupply)
        {
            var vatTu = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId);
            if (vatTu == null)
            {
                throw new NotFoundException(vatTu.GetTypeEx(), vatTuId.ToString());
            }
            
            response.TenVatTu = vatTu.TenVatTu;
            response.ThongSoKyThuat = vatTu.MoTa ?? string.Empty;
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
        //if Id is VatTuNewId => get information from QlvtMuaSamVatTuNew table
        var vatTuNew = await _muaSamVatTuNewRepository.GetAsync(vatTuId);
        if (vatTuNew == null) return response;
        response.TenVatTu = vatTuNew.TenVatTu;
        response.ThongSoKyThuat = vatTuNew.ThongSoKyThuat ?? string.Empty;
        return response;
    }

    public async Task<int> CreateSupplyAsync(CreatedSupplyRequest request)
    {   
        await ValidationHelper.ValidateAsync(request, new CreatedSupplyRequestValidation());
        var vatTu = request.Adapt<QlvtMuaSamVatTuNew>();
        return await _muaSamVatTuNewRepository.InsertAsync(vatTu);  
    }

    public async Task<int> CreateSupplyTicketAsync()
    {
        var supplyTicket = new QlvtMuaSamPhieuDeXuat();
        supplyTicket.TenPhieu = $"Phiếu yêu cầu cung ứng vật tư {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        supplyTicket.TrangThai = (byte?)PurchaseOrderStatus.Unsigned;
        supplyTicket.NgayThem = DateTime.Now;
        supplyTicket.MaNguoiThem = _authorizedContextFacade.AccountId;
        await _muaSamPhieuDeXuatRepository.InsertAsync(supplyTicket);
        
        var addedSupplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.TenPhieu == supplyTicket.TenPhieu);
        if (addedSupplyTicket == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.InsertFailed);
        }
        return addedSupplyTicket.Id;
    }

    public async Task<IEnumerable<SupplyTicketListResponseItem>> ListSupplyTicketAsync()
    {
        var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync())
            .Adapt<IEnumerable<SupplyTicketListResponseItem>>();
        return listPhieu;
    }

    public async Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests)
     {
        #region Validate
        if (supplyTicketId <=0 )
        {
            throw new BadRequestException(Constants.Exceptions.Messages.SupplyTicket.InvalidId);
        }
        if (!requests.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        var supplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.Id == supplyTicketId);
        if (supplyTicket == null)
        {
            throw new NotFoundException(supplyTicket.GetTypeEx(), supplyTicketId.ToString());
        }
        #endregion
        //var supplyTicketDetail = new QlvtMuaSamPhieuDeXuat();
        var listSupplyTicketDetail = new List<QlvtMuaSamPhieuDeXuatDetail>();
        var groupedRequests = requests
            .GroupBy(x => new { x.IdVatTu, x.IsSystemSupply })
            .Select(x => new SupplyTicketDetailRequest
            {
                IdVatTu = x.Key.IdVatTu,
                IsSystemSupply = x.Key.IsSystemSupply,
                SoLuong = x.Sum(r => r.SoLuong), 
                TenVatTu = x.First().TenVatTu,
                ThongSoKyThuat =  x.First().ThongSoKyThuat,
                GhiChu = x.First().GhiChu,
                DonViTinh = x.First().DonViTinh,
            })
            .ToList();
        foreach (var vatTu in groupedRequests)
        {   
            var supplyTicketDetail = vatTu.Adapt<QlvtMuaSamPhieuDeXuatDetail>();
            supplyTicketDetail.PhieuDeXuatId = supplyTicketId;
            listSupplyTicketDetail.Add(supplyTicketDetail);
        }
        return await _muaSamPhieuDeXuatDetailRepository.InsertManyAsync(listSupplyTicketDetail);
    }
}