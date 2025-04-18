﻿using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.GioHang.Responses;
using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;
using ESPlatform.QRCode.IMS.Core.Validations.VatTus;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Infra.Repositories;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace ESPlatform.QRCode.IMS.Core.Services.GioHang;

public class GioHangService : IGioHangService
{
    private readonly INguoiDungService _nguoiDungService;
    private readonly IGioHangRepository _gioHangRepository;
    private readonly IVatTuRepository _vatTuRepository;
    private readonly IVatTuTonKhoRepository _vatTuTonKhoRepository;
    private readonly IMuaSamVatTuNewRepository _muaSamVatTuNewRepository;
    private readonly IMapper _mapper;
    private readonly ImagePath _imagePath;

    public GioHangService(
        INguoiDungService nguoiDungService,
        IGioHangRepository gioHangRepository,
        IVatTuRepository vatTuRepository,
        IVatTuTonKhoRepository vatTuTonKhoRepository,
        IMuaSamVatTuNewRepository muaSamVatTuNewRepository,
        IMapper mapper,
        IOptions<ImagePath> imagePath)
    {
        _nguoiDungService = nguoiDungService;
        _gioHangRepository = gioHangRepository;
        _vatTuRepository = vatTuRepository;
        _muaSamVatTuNewRepository = muaSamVatTuNewRepository;
        _vatTuTonKhoRepository = vatTuTonKhoRepository;
        _mapper = mapper;
        _imagePath = imagePath.Value;
    }

    public async Task<int> GetSupplyCountAsync()
    {
        var currentUser = await _nguoiDungService.GetCurrentUserAsync();
        var currentUserId = currentUser.MaNguoiDung;
        return await _gioHangRepository.CountAsync(x => x.UserId == currentUserId);
    }

    public async Task<IEnumerable<CartSupplyResponse>> ListSupplyAsync()
    {
        var currentUser = await _nguoiDungService.GetCurrentUserAsync();
        var userId = currentUser.MaNguoiDung;
        var vatTus = (await _gioHangRepository.ListSupplyAsync(userId, _imagePath.RelativeBasePath)).Adapt<IEnumerable<CartSupplyResponse>>().ToList();
        if (!vatTus.Any())
        {
            return new List<CartSupplyResponse>();
        }
        foreach (var item in vatTus.Where(item => item.IsSystemSupply))
        {
            item.Image = GetSupplyImage(item.VatTuId);
        }
        return vatTus;
    }

    public async Task<int> ModifyQuantityAsync(int gioHangId, decimal quantity)
    {
        if (gioHangId < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }

        if (quantity < 1)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.InvalidQuantity);
        }
        var supplyInCart = await _gioHangRepository.GetAsync(x => x.GioHangId == gioHangId);
        if (supplyInCart == null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Cart.SupplyNotExist);
        }
        supplyInCart.SoLuong = quantity;
        supplyInCart.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(supplyInCart);
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
        if (supplyInCart.IsSystemSupply == true)
        {
            var supply = await _vatTuRepository.GetAsync(x => x.VatTuId == supplyInCart.VatTuId);
            // vật tư không tồn tại => xóa trong giỏ hàng
            if (supply == null)
            {
                await _gioHangRepository.DeleteAsync(supplyInCart);
                throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
            }

            if (request.GhiChu != null)
            {
                supplyInCart.GhiChu = request.GhiChu;
            }

            if (request.SoLuong != null)
            {
                supplyInCart.SoLuong = request.SoLuong;
            }

            if (request.ThongSoKyThuat != null)
            {
                supplyInCart.ThongSoKyThuat = request.ThongSoKyThuat;
            }
            supplyInCart.ThoiGianCapNhat = DateTime.Now;
            return await _gioHangRepository.UpdateAsync(supplyInCart);
        }
        // Nếu là vật tư mới thêm => ktra xem vật tư còn trong bảng vật tư mới không
        var supplyNew = await _muaSamVatTuNewRepository.GetAsync(x => x.VatTuNewId == supplyInCart.VatTuId);
         //Vật tư không tồn tại => xóa trong giỏ hàng
         if (supplyNew == null)
         {
             await _gioHangRepository.DeleteAsync(supplyInCart);
             throw new NotFoundException(Constants.Exceptions.Messages.Cart.DeletedSupply);
         }
         //Cập nhật lại thông tin trong bảng giỏ hàng
         if (request.GhiChu != null)
         {
             supplyInCart.GhiChu = request.GhiChu;
         }

         if (request.SoLuong != null)
         {
             supplyInCart.SoLuong = request.SoLuong;
         }

         if (request.ThongSoKyThuat != null)
         {
             supplyInCart.ThongSoKyThuat = request.ThongSoKyThuat;
         }
        supplyInCart.ThoiGianCapNhat = DateTime.Now;
        return await _gioHangRepository.UpdateAsync(supplyInCart);
    }
    public async Task<int> CreateCartSupplyAsync(int vatTuId, CreatedCartSupplyRequest request)
    {
        var currentUser = await _nguoiDungService.GetCurrentUserAsync();
        var userId = currentUser.MaNguoiDung;
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
        var existedTonKho = await _vatTuTonKhoRepository.ExistsAsync(x => x.VatTuId == vatTuId);
        supplyToAdd.Is007a = existedTonKho ? (short)Is007A.TonKho : (short)Is007A.KhongTonKho;
        supplyToAdd.VatTuId = vatTuId;
        supplyToAdd.UserId = userId;
        supplyToAdd.ThoiGianTao = DateTime.Now;
        return await _gioHangRepository.InsertAsync(supplyToAdd);
    }
    public async Task<int> CreateCartSupplyNewAsync(CreatedSupplyRequest request)
    {   
        await ValidationHelper.ValidateAsync(request, new CreatedSupplyRequestValidation());
        var existedVatTuNew = await _muaSamVatTuNewRepository.ExistsAsync(x => x.TenVatTu.ToLower() == request.TenVatTu.ToLower());
        if (existedVatTuNew)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Supplies.DuplicatedSupplyName);
        }

        var existedVatTu = await _vatTuRepository.ExistsAsync(x => x.TenVatTu != null && x.TenVatTu.ToLower() == request.TenVatTu.ToLower());
        if (existedVatTu)
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

    public string GetSupplyImage(int vatTuId)
    {
        // Các đuôi file được phép
        string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" }; 
        // Xây dựng dường dẫn đến folder hoàn chỉnh
        var rootPath = _imagePath.RootPath; 
        var relativeBasePath = _imagePath.RelativeBasePath; 
        var localBasePath = (rootPath + relativeBasePath).Replace("/", "\\"); 
        var localFolder = Path.Combine(localBasePath, vatTuId.ToString());
        if (!Directory.Exists(localFolder)) return string.Empty;
        
        // lấy danh sách các file trong folder có đuôi ảnh
        var imageFiles = Directory.GetFiles(localFolder)
            .Where(x => allowedExtensions.Contains(Path.GetExtension(x).ToLower()))
            .ToList();
        
        if (imageFiles.Count == 0) return string.Empty;
        //lấy ảnh đầu tiên trong file làm ảnh đại diện
        var firstImageFile = imageFiles.First();
        return Path.Combine(relativeBasePath, vatTuId.ToString(), Path.GetFileName(firstImageFile)).Replace("\\", "/");
    }
}