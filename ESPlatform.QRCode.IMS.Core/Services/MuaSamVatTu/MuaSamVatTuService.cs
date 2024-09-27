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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IMuaSamPhieuDeXuatRepository _muaSamPhieuDeXuatRepository;
    private readonly IMuaSamPhieuDeXuatDetailRepository _muaSamPhieuDeXuatDetailRepository;
    private readonly IKhoRepository _khoRepository;
    private readonly IGioHangRepository _gioHangRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;

    public MuaSamVatTuService(
        IVatTuRepository vatTuRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMuaSamPhieuDeXuatRepository muaSamPhieuDeXuatRepository,
        IMuaSamPhieuDeXuatDetailRepository muaSamPhieuDeXuatDetailRepository,
        IKhoRepository khoRepository,
        IGioHangRepository gioHangRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IUnitOfWork unitOfWork)
    {
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _muaSamPhieuDeXuatRepository = muaSamPhieuDeXuatRepository;
        _muaSamPhieuDeXuatDetailRepository = muaSamPhieuDeXuatDetailRepository;
        _khoRepository = khoRepository;
        _gioHangRepository = gioHangRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _unitOfWork = unitOfWork;
        
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

    public async Task<SupplyOrderDetailResponse> GetPurchaseSupplyAsync(int vatTuId, bool isSystemSupply)
    {
        if (vatTuId <= 0 )
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        
        var response = new SupplyOrderDetailResponse();
        // if Id is VatTuId => get information from QlvtVatTu table
        if (isSystemSupply)
        {
            var vatTu = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId);
            if (vatTu == null)
            {
                throw new NotFoundException(vatTu.GetTypeEx(), null);
            }
            
            response.TenVatTu = vatTu.TenVatTu ?? string.Empty;
            response.ThongSoKyThuat = vatTu.MoTa ?? string.Empty;
            response.GhiChu = vatTu.GhiChu ?? string.Empty;
            var folderPath = AppConfig.Instance.Image.FolderPath;               // "D:"
            var urlPath = AppConfig.Instance.Image.UrlPath;                     // "/Images"
            var localBasePath =  (folderPath + urlPath).Replace("/", "\\");     // "D:\Images"
            var folderImagePath = $@"{localBasePath}\{vatTuId}";
            //var urlPath = $"{AppConfig.Instance.Image.UrlPath}/{vatTuId}";
            if (Directory.Exists(folderImagePath))
            {
                var imageFiles = Directory.GetFiles(folderImagePath);

                foreach (var file in imageFiles)
                {
                    var fileName = Path.GetFileName(file);
                    var fullPath = Path.Combine(urlPath, vatTuId.ToString(), fileName).Replace("\\", "/");
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

    public async Task<int> CreateSupplyTicketAsync(CreatedSupplyTicketRequest request)
    {
        if (!request.SupplyTicketDetails.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        // Bắt đầu transaction
        using (var transaction = _unitOfWork.BeginTransactionAsync())
        {
            try
            {
                var supplyTicket = new QlvtMuaSamPhieuDeXuat
                {
                    TenPhieu = $"Phiếu yêu cầu cung ứng vật tư {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                    MoTa = request.Description,
                    TrangThai = (byte?)SupplyTicketStatus.Unsigned,
                    NgayThem = DateTime.Now,
                    MaNguoiThem = _authorizedContextFacade.AccountId
                };
                await _muaSamPhieuDeXuatRepository.InsertAsync(supplyTicket);

                var addedSupplyTicket =
                    await _muaSamPhieuDeXuatRepository.GetAsync(x => x.TenPhieu == supplyTicket.TenPhieu);
                if (addedSupplyTicket == null)
                {
                    throw new BadRequestException(Constants.Exceptions.Messages.Common.InsertFailed);
                }
                // thêm các vật tư đã chọn trong giỏ hàng vào phiếu
                var supplyTicketId = addedSupplyTicket.Id;
                await CreateManySupplyTicketDetailAsync(supplyTicketId, request.SupplyTicketDetails);
                
                // xóa các vật tư vừa chọn trong giỏ hàng
                var gioHangIds = request.SupplyTicketDetails.Select(supplyTicketDetail => supplyTicketDetail.GioHangId).ToList();
                var suppliesInCart = await _gioHangRepository.ListAsync(x => gioHangIds.Contains(x.GioHangId));
                await _gioHangRepository.DeleteManyAsync(suppliesInCart);
                await _unitOfWork.CommitAsync();
                return supplyTicketId;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }

    public async Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync(PhraseAndPagingFilter requests)
    {
        var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync(
                string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower(),
                requests.GetPageIndex(),
                requests.GetPageSize()))
            .Adapt<PagedList<SupplyTicketListResponseItem>>();
        return listPhieu;
    }

    private async Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests)
     {
        if (!requests.Any())
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.EmptySupplies);
        }
        var listSupplyTicketDetail = new List<QlvtMuaSamPhieuDeXuatDetail>();
        foreach (var supplyCart in requests)
        {
            if (supplyCart.GioHangId < 1)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidCartInfo, new List<string>{ nameof(supplyCart.GioHangId)+ " is invalid"});
            }

            if (supplyCart.VatTuId < 1)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidCartInfo, new List<string>{ nameof(supplyCart.VatTuId)+ " is invalid"});
            }
            await ValidationHelper.ValidateAsync(supplyCart, new SupplyTicketDetailRequestValidation());
            var supplyTicketDetail = supplyCart.Adapt<QlvtMuaSamPhieuDeXuatDetail>();
            supplyTicketDetail.PhieuDeXuatId = supplyTicketId;
            listSupplyTicketDetail.Add(supplyTicketDetail);
        }
        return await _muaSamPhieuDeXuatDetailRepository.InsertManyAsync(listSupplyTicketDetail);
    }

    public async Task<SupplyTicketDetailResponse> GetSupplyTicketDetailAsync(int supplyTicketId)
    {
        var response = new SupplyTicketDetailResponse();
        if (supplyTicketId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicket,
                new List<string> { nameof(supplyTicketId) + " is invalid" });
        }
        var supplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(x => x.Id == supplyTicketId);
        if (supplyTicket == null)
        {
            throw new NotFoundException(supplyTicket.GetTypeEx(), null);
        }
        response.TenPhieu = supplyTicket.TenPhieu ?? string.Empty;
        response.MoTa = supplyTicket.MoTa ?? string.Empty;
        var listSupplies = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(supplyTicketId))
            .Adapt<IEnumerable<SupplyResponse>>();
        response.DanhSachVatTu = listSupplies.ToList();
        response.Tong = response.DanhSachVatTu.Count;
        return response;
    }

    public async Task<int> DeleteSupplyTicketAsync(int supplyTicketId)
    {
        if (supplyTicketId <= 0)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicket);
        }
        var currentSupplyTicket = await _muaSamPhieuDeXuatRepository.GetAsync(supplyTicketId);
        if (currentSupplyTicket == null)
        {
            throw new NotFoundException(currentSupplyTicket.GetTypeEx(), null);
        }
        // var currentSupplyTicketDetails = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(x => x.PhieuDeXuatId == supplyTicketId)).ToList();
        // if (currentSupplyTicketDetails.Any())
        // {
        //     await _muaSamPhieuDeXuatDetailRepository.DeleteManyAsync(currentSupplyTicketDetails);
        // }
        currentSupplyTicket.TrangThai = (byte?)SupplyTicketStatus.Deleted;
        currentSupplyTicket.NgaySua = DateTime.Now;
        return await _muaSamPhieuDeXuatRepository.UpdateAsync(currentSupplyTicket);
    }

    public async Task<IEnumerable<WarehouseResponseItem>> ListWarehousesAsync()
    {
        var response = (await _khoRepository.ListWarehousesAsync()).Adapt<IEnumerable<WarehouseResponseItem>>();
        return response;
    }
}