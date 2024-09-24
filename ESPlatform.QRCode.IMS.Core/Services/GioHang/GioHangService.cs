using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using Mapster;
using MapsterMapper;

namespace ESPlatform.QRCode.IMS.Core.Services.GioHang;

public class GioHangService : IGioHangService
{
    private readonly IGioHangRepository _gioHangRepository;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly IMapper _mapper;

    public GioHangService(
        IGioHangRepository gioHangRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IVatTuRepository vatTuRepository
        ,IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMapper mapper)
    {
        _gioHangRepository = gioHangRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _mapper = mapper;
    }

    public async Task<int> GetSupplyCountAsync()
    {
        var userId = _authorizedContextFacade.AccountId;
        return await _gioHangRepository.CountAsync(x => x.UserId == userId);
    }

    public async Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync()
    {
        var userId = _authorizedContextFacade.AccountId;
        return (await _gioHangRepository.ListSupplyAsync(userId)).Adapt<IEnumerable<CartSupplyResponse>>();
    }

    public async Task<int> ModifyQuantityAsync(int vatTuId, int quantity)
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }

        if (quantity < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidQuantity);
        }
        var vatTu = await _gioHangRepository.GetAsync(x => x.VatTuId == vatTuId && x.UserId == _authorizedContextFacade.AccountId);
        if (vatTu == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        vatTu.SoLuong = quantity;
        vatTu.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(vatTu);
    }

    public async Task<int> DeleteSupplyAsync(int vatTuId)
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        var vatTu = await _gioHangRepository.GetAsync(x => x.VatTuId == vatTuId && x.UserId == _authorizedContextFacade.AccountId);
        if (vatTu == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        return await _gioHangRepository.DeleteAsync(vatTu);
    }

    public async Task<int> ModifyInformationAsync(int vatTuId, ModifiedCartSupplyRequest request )
    {
        if (vatTuId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.InvalidSupply);
        }
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.VatTuId == vatTuId && x.UserId == _authorizedContextFacade.AccountId);
        if (supplyInCart == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        // Nếu là vật tư => ktra xem vật tư còn trong hệ thống không
        if (request.IsSystemSupply)
        {
            var supply = await _vatTuRepository.GetAsync(x => x.VatTuId == vatTuId);
            // vật tư không tồn tại => xóa trong giỏ hàng
            if (supply == null)
            {
                await _gioHangRepository.DeleteAsync(supplyInCart);
                throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
            }
            // vật tư tồn tại => cập nhật lại thông tin
            supply.MoTa = request.ThongSoKyThuat;
            supply.GhiChu = request.GhiChu;
            await _vatTuRepository.UpdateAsync(supply);
            
            // Cập nhật lại thông tin trong bảng giỏ hàng
            supplyInCart.ThongSoKyThuat = request.ThongSoKyThuat;
            supplyInCart.GhiChu = request.GhiChu;
            supplyInCart.ThoiGianCapNhat = DateTime.Now;
            return await _gioHangRepository.UpdateAsync(supplyInCart);
        }
        // Nếu là vật tư mới thêm => ktra xem vật tư còn trong bảng vật tư mới không
        var supplyNew = await _muaSamVatTuNewRepository.GetAsync(x => x.VatTuNewId == vatTuId);
        // Vật tư không tồn tại => xóa trong giỏ hàng
        if (supplyNew == null)
        {
            await _gioHangRepository.DeleteAsync(supplyInCart);
            throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
        }
        // Vật tư tồn tại => cập nhật lại thông tin
        supplyNew.ThongSoKyThuat = request.ThongSoKyThuat;
        supplyNew.GhiChu = request.GhiChu;
        await _muaSamVatTuNewRepository.UpdateAsync(supplyNew);
        
        // Cập nhật lại thông tin trong bảng giỏ hàng
        supplyInCart.ThongSoKyThuat = request.ThongSoKyThuat;
        supplyInCart.GhiChu = request.GhiChu;
        supplyInCart.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(supplyInCart);
    }
    public async Task<int> CreateSupplyAsync(int vatTuId, CreatedCartSupplyRequest request)
    {
        var userId = _authorizedContextFacade.AccountId;
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.VatTuId == vatTuId && x.UserId == userId && x.IsSystemSupply == request.IsSystemSupply);
        if (supplyInCart != null)
        {
            supplyInCart.SoLuong = supplyInCart.SoLuong + request.SoLuong;
            supplyInCart.ThoiGianCapNhat = DateTime.Now;
            return await _gioHangRepository.UpdateAsync(supplyInCart);
        }

        var supplyToAdd = _mapper.Map<QlvtGioHang>(request);
        supplyToAdd.VatTuId = vatTuId;
        supplyToAdd.UserId = userId;
        supplyToAdd.ThoiGianTao = DateTime.Now;
        return await _gioHangRepository.InsertAsync(supplyToAdd);
    }
}