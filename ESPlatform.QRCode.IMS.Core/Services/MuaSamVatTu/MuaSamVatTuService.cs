using System.Globalization;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.TraCuu.Responses;
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
using MapsterMapper;
using Microsoft.Extensions.Options;

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
    private readonly IVanBanKyRepository _vanBanKyRepository;
    private readonly IVatTuBoMaRepository _vatTuBoMaRepository;
    private readonly IMuaSamPdxKyRepository _muaSamPdxKyRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly ImagePath _imagePath;
    private readonly IMapper _mapper;

    public MuaSamVatTuService(
        IVatTuRepository vatTuRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMuaSamPhieuDeXuatRepository muaSamPhieuDeXuatRepository,
        IMuaSamPhieuDeXuatDetailRepository muaSamPhieuDeXuatDetailRepository,
        IKhoRepository khoRepository,
        IGioHangRepository gioHangRepository,
        IVanBanKyRepository vanBanKyRepository,
        IVatTuBoMaRepository vatTuBoMaRepository,
        IMuaSamPdxKyRepository muaSamPdxKyRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IUnitOfWork unitOfWork,
        IOptions<ImagePath> imagePath,
        IMapper mapper)
    {
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _muaSamPhieuDeXuatRepository = muaSamPhieuDeXuatRepository;
        _muaSamPhieuDeXuatDetailRepository = muaSamPhieuDeXuatDetailRepository;
        _khoRepository = khoRepository;
        _gioHangRepository = gioHangRepository;
        _vanBanKyRepository = vanBanKyRepository;
        _vatTuBoMaRepository = vatTuBoMaRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _muaSamPdxKyRepository = muaSamPdxKyRepository;
        _imagePath = imagePath.Value;
    }

    public async Task<PagedList<SupplyListResponseItem>> ListVatTuAsync(SupplyListRequest request)
    {
        await ValidationHelper.ValidateAsync(request, new SupplyListRequestValidation());
        var relativeBasePath = _imagePath.RelativeBasePath;
        var listVatTu = (await _vatTuRepository.ListAsync(
                string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                request.IdKho,
                request.ListIdToMay,
                request.ListIdGiaKe,
                request.ListIdNgan,
                request.ListMaNhom,
                relativeBasePath,
                request.GetPageIndex(),
                request.GetPageSize()))
            .Adapt<PagedList<SupplyListResponseItem>>();
        if (listVatTu.Total == 0 || request.IsSystemSupply == false)
        {
            var listVatTuNew = (await _muaSamVatTuNewRepository.ListAsync(
                    string.IsNullOrWhiteSpace(request.TenVatTu) ? string.Empty : request.TenVatTu.ToLower(),
                    string.IsNullOrWhiteSpace(request.MaVatTu) ? string.Empty : request.MaVatTu.ToLower(),
                    request.GetPageIndex(),
                    request.GetPageSize()))
                .Adapt<PagedList<SupplyListResponseItem>>();
            return listVatTuNew;
        }
        return listVatTu;
    }

    public async Task<SupplyOrderDetailResponse> GetSupplyOrderDetailAsync(int vatTuId, bool isSystemSupply)
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
            response.DonGia = vatTu.DonGia ?? 0;
            var rootPath = _imagePath.RootPath;              
            var relativeBasePath = _imagePath.RelativeBasePath;             
            var localBasePath =  (rootPath + relativeBasePath).Replace("/", "\\"); 
            var folderImagePath = $@"{localBasePath}\{vatTuId}";
            if (Directory.Exists(folderImagePath))
            {
                var imageFiles = Directory.GetFiles(folderImagePath);

                foreach (var file in imageFiles)
                {
                    var fileName = Path.GetFileName(file);
                    var fullPath = Path.Combine(relativeBasePath, vatTuId.ToString(), fileName).Replace("\\", "/");
                    response.ImagePaths.Add(fullPath);
                }
            }
            var wareHouse = await _vatTuRepository.GetLotNumberAsync(vatTuId, vatTu.KhoId);
            if (wareHouse == null) return response;
            var wareHouseMapper = _mapper.Map<LookupSuppliesResponse>(wareHouse);
            response.OnhandQuantity = wareHouseMapper.OnhandQuantity;
            return response;
        }
        //if Id is VatTuNewId => get information from QlvtMuaSamVatTuNew table
         var vatTuNew = await _muaSamVatTuNewRepository.GetAsync(vatTuId);
         if (vatTuNew == null)
         {
             throw new NotFoundException(vatTuNew.GetTypeEx(), null);
         }
         response.TenVatTu = vatTuNew.TenVatTu ?? string.Empty;
         response.ThongSoKyThuat = vatTuNew.ThongSoKyThuat ?? string.Empty;
         response.DonGia = vatTuNew.DonGia ?? 0;
         response.GhiChu = vatTuNew.GhiChu ?? string.Empty;
         response.OnhandQuantity = 0;
         return response;
    }

    public async Task<int> ProcessSupplyTicketCreationAsync(ProcessSupplyTicketCreationRequest request)
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
                var supplyTicketName = await CreateSupplyTicketAsync(request.Description);

                var addedSupplyTicket =
                    await _muaSamPhieuDeXuatRepository.GetAsync(x => x.TenPhieu == supplyTicketName);
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
                
                // thêm mới 2 phiếu vào bảng ký
                await CreateTwoTextToSignAsync(supplyTicketId);
                
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

    public async Task<PagedList<SupplyTicketListResponseItem>> ListSupplyTicketAsync(SupplyTicketRequest requests)
    {
        if (requests.SupplyTicketStatus is <= SupplyTicketStatus.Unknown or >= SupplyTicketStatus.Deleted)
        {
            throw new ArgumentException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicketStatus);
        }
        var listPhieu = (await _muaSamPhieuDeXuatRepository.ListSupplyTicketAsync(
                string.IsNullOrWhiteSpace(requests.Keywords) ? string.Empty : requests.Keywords.ToLower(),
                requests.SupplyTicketStatus ?? SupplyTicketStatus.Unknown,
                requests.GetPageIndex(),
                requests.GetPageSize()))
            .Adapt<PagedList<SupplyTicketListResponseItem>>();
        return listPhieu;
    }

    private async Task<int> CreateManySupplyTicketDetailAsync(int supplyTicketId, List<SupplyTicketDetailRequest> requests)
     {
         var relativeBasePath = _imagePath.RelativeBasePath;
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
        var listSupplies = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(supplyTicketId)).Adapt<IEnumerable<SupplyResponse>>().ToList();
        if (!listSupplies.Any())
        {
            response.DanhSachVatTu = listSupplies;
            response.Tong = 0;
            return response;
        }
        
        var relativeBasePath = _imagePath.RelativeBasePath;
        foreach (var supply in listSupplies)
        {
            if (!string.IsNullOrWhiteSpace(supply.Image))
            {
                supply.Image = relativeBasePath + supply.Image;
            }
        }
        response.DanhSachVatTu = listSupplies;
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
        var currentSupplyTicketDetails = (await _muaSamPhieuDeXuatDetailRepository.ListAsync(x => x.PhieuDeXuatId == supplyTicketId)).ToList();
        if (currentSupplyTicketDetails.Any())
        {
            await _muaSamPhieuDeXuatDetailRepository.DeleteManyAsync(currentSupplyTicketDetails);
        }

        var currentSignedDocuments = (await _vanBanKyRepository.ListAsync(x => x.PhieuId == supplyTicketId)).ToList();
        if (currentSignedDocuments.Any())
        {
            await _vanBanKyRepository.DeleteManyAsync(currentSignedDocuments);
        }

        var currentSignedSupplyTickets = (await _muaSamPdxKyRepository.ListAsync(x => x.PhieuDeXuatId == supplyTicketId)).ToList();
        if (currentSignedSupplyTickets.Any())
        {
            await _muaSamPdxKyRepository.DeleteManyAsync(currentSignedSupplyTickets);
        }
        // currentSupplyTicket.TrangThai = (byte?)SupplyTicketStatus.Deleted;
        // currentSupplyTicket.NgaySua = DateTime.Now;
        return await _muaSamPhieuDeXuatRepository.DeleteAsync(currentSupplyTicket);
    }

    public async Task<IEnumerable<WarehouseResponseItem>> ListWarehousesAsync()
    {
        var response = (await _khoRepository.ListWarehousesAsync()).Adapt<IEnumerable<WarehouseResponseItem>>();
        return response;
    }

    private async Task<string> CreateSupplyTicketAsync(string? description)
    {
        var supplyTicket = new QlvtMuaSamPhieuDeXuat
        {
            TenPhieu = $"Phiếu yêu cầu cung ứng vật tư {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
            MoTa = description,
            TrangThai = (byte?)SupplyTicketStatus.Unsigned,
            NgayThem = DateTime.Now,
            MaNguoiThem = _authorizedContextFacade.AccountId
        };
        await _muaSamPhieuDeXuatRepository.InsertAsync(supplyTicket);
        return supplyTicket.TenPhieu;
    }

    private async Task<int> CreateTwoTextToSignAsync(int supplyTicketId)
    {
        var textTosign = new List<QlvtVanBanKy>()
        {
            new QlvtVanBanKy
            {
                PhieuId = supplyTicketId,
                MaLoaiVanBan = "PhieuDeXuat",
                NgayTao = DateTime.Now
            },
            new QlvtVanBanKy
            {
                PhieuId = supplyTicketId,
                MaLoaiVanBan = "PhieuDuyet",
                NgayTao = DateTime.Now
            }
        };
        return await _vanBanKyRepository.InsertManyAsync(textTosign);
    }
    
    public async Task<IEnumerable<QlvtVatTuBoMa>> ListGroupCodeAsync()
    {
        return await _vatTuBoMaRepository.ListAsync();
    }

    public async Task<int> CountSupplyTicketsByStatusAsync(SupplyTicketStatus status)
    {
        if (status is <= SupplyTicketStatus.Unknown or > SupplyTicketStatus.Deleted)
        {
            throw new ArgumentException(Constants.Exceptions.Messages.SupplyTicket.InvalidSupplyTicketStatus);
        }
        return await _muaSamPhieuDeXuatRepository.CountAsync(x => x.TrangThai == (byte)status);
    }

    public async Task<IEnumerable<string>> ListCreatedSupplyTicketWarningAsync(List<int> vatTuIds)
    {
        var existingSupplyTickets = (await _muaSamPhieuDeXuatRepository.ListCreatedSupplyTicketWarningAsync(vatTuIds)).Adapt<List<SupplyTicketWithTotalCount>>();
        if (!existingSupplyTickets.Any())
        {
            return Enumerable.Empty<string>();
        }

        return existingSupplyTickets
            .Select(x => $"Vật tư {x.TenVatTu} đã được đề xuất với số lượng {x.SoLuong.FormatDecimal()} trong 1 phiếu khác.")
            .ToList();
    }
}