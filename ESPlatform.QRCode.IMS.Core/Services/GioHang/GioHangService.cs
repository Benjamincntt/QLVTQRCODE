using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace ESPlatform.QRCode.IMS.Core.Services.GioHang;

public class GioHangService : IGioHangService
{
    private readonly IGioHangRepository _gioHangRepository;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IMapper _mapper;
    private readonly ImagePath _imagePath;

    public GioHangService(
        IGioHangRepository gioHangRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IVatTuRepository vatTuRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMapper mapper,
        IOptions<ImagePath> imagePath)
    {
        _gioHangRepository = gioHangRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _mapper = mapper;
        _imagePath = imagePath.Value;
    }

    public async Task<int> GetSupplyCountAsync()
    {
        var userId = _authorizedContextFacade.AccountId;
        return await _gioHangRepository.CountAsync(x => x.UserId == userId);
    }

    public async Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync()
    {
        var userId = _authorizedContextFacade.AccountId;
        return (await _gioHangRepository.ListSupplyAsync(userId, _imagePath.RelativeBasePath)).Adapt<IEnumerable<CartSupplyResponse>>();
    }

    public async Task<int> ModifyQuantityAsync(int gioHangId, int quantity)
    {
        if (gioHangId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }

        if (quantity < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidQuantity);
        }
        var vatTu = await _gioHangRepository.GetAsync(x => x.GioHangId == gioHangId);
        if (vatTu == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        vatTu.SoLuong = quantity;
        vatTu.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(vatTu);
    }

    public async Task<int> DeleteSupplyAsync(int gioHangId)
    {
        if (gioHangId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.GioHangId == gioHangId);
        if (supplyInCart == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        return await _gioHangRepository.DeleteAsync(supplyInCart);
    }

    public async Task<int> ModifyInformationAsync(int gioHangId, ModifiedCartSupplyRequest request )
    {
        if (gioHangId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.GioHangId == gioHangId);
        if (supplyInCart == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        // Nếu là vật tư => ktra xem vật tư còn trong hệ thống không
        // if (supplyInCart.IsSystemSupply == true)
        // {
            var supply = await _vatTuRepository.GetAsync(x => x.VatTuId == supplyInCart.VatTuId);
            // vật tư không tồn tại => xóa trong giỏ hàng
            if (supply == null)
            {
                await _gioHangRepository.DeleteAsync(supplyInCart);
                throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
            }
        //}
        // Nếu là vật tư mới thêm => ktra xem vật tư còn trong bảng vật tư mới không
        //var supplyNew = await _muaSamVatTuNewRepository.GetAsync(x => x.VatTuNewId == supplyInCart.VatTuId);
        // Vật tư không tồn tại => xóa trong giỏ hàng
        // if (supplyNew == null)
        // {
        //     await _gioHangRepository.DeleteAsync(supplyInCart);
        //     throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
        // }
        // Cập nhật lại thông tin trong bảng giỏ hàng
        //supplyInCart.ThongSoKyThuat = request.ThongSoKyThuat;
        supplyInCart.GhiChu = request.GhiChu;
        supplyInCart.SoLuong = request.SoLuong;
        supplyInCart.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(supplyInCart);
    }
    public async Task<int> CreateCartSupplyAsync(int vatTuId, CreatedCartSupplyRequest request)
    {
        var userId = _authorizedContextFacade.AccountId;
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.VatTuId == vatTuId && x.UserId == userId && x.IsSystemSupply == request.IsSystemSupply);
        if (supplyInCart != null)
        {
            supplyInCart.SoLuong = supplyInCart.SoLuong + request.SoLuong;
            supplyInCart.ThoiGianCapNhat = DateTime.Now;
            return await _gioHangRepository.UpdateAsync(supplyInCart);
        }
        
        // thêm mới giỏ hàng
        var supplyToAdd = _mapper.Map<QlvtGioHang>(request);
        if (request.IsSystemSupply)
        {
            var supply = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId);
            if (supply == null)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Supplies.SupplyNotExist);
            }
        }
        else
        {
            var supplyNew = await _muaSamVatTuNewRepository.GetAsync(x => x.VatTuNewId == vatTuId);
            if (supplyNew == null)
            {
                throw new BadRequestException(Constants.Exceptions.Messages.Supplies.SupplyNotExist);
            }
        }
        
        supplyToAdd.VatTuId = vatTuId;
        supplyToAdd.UserId = userId;
        supplyToAdd.ThoiGianTao = DateTime.Now;
        return await _gioHangRepository.InsertAsync(supplyToAdd);
    }
    public async Task<int> CreateSupplyAsync(CreatedSupplyRequest request)
    {   
        await ValidationHelper.ValidateAsync(request, new CreatedSupplyRequestValidation());
        var existVatTuNew = await _muaSamVatTuNewRepository.ExistsAsync(x => x.TenVatTu.ToLower() == request.TenVatTu.ToLower());
        if (existVatTuNew)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.DuplicatedSupplyName);
        }

        request.MaVatTu = request.MaVatTu.Trim();
        var vatTu = request.Adapt<QlvtMuaSamVatTuNew>();
        await _muaSamVatTuNewRepository.InsertAsync(vatTu);
        var vatTuAdded = await _muaSamVatTuNewRepository.GetAsync(x => x.TenVatTu == vatTu.TenVatTu);
        if (vatTuAdded == null)
        {
            return default;
        }
        //thêm vật tư mới vào giỏ hàng luôn
        var createdCartSupplyRequest = new CreatedCartSupplyRequest()
        {
            SoLuong = request.SoLuong,
            ThongSoKyThuat = vatTuAdded.ThongSoKyThuat ?? string.Empty,
            GhiChu = vatTuAdded.GhiChu ?? string.Empty,
            IsSystemSupply = false
        };
        return await CreateCartSupplyAsync(vatTuAdded.VatTuNewId, createdCartSupplyRequest);
    }
}