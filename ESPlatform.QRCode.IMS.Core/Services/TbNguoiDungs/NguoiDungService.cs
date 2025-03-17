using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Core.Validations.NguoiDungs;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Mapster;

namespace ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;

public class NguoiDungService : INguoiDungService
{
    private readonly INguoiDungRepository _nguoiDungRepository;
    private readonly IDonViSuDungRepository _donviDungRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;

    public NguoiDungService(
        INguoiDungRepository nguoiDungRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IDonViSuDungRepository donviDungRepository)
    {
        _nguoiDungRepository = nguoiDungRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _donviDungRepository = donviDungRepository;
    }

    public async Task<int> ModifyAsync(int maNguoiDung, ModifiedUserRequest request)
    {
        var nguoiDung = await _nguoiDungRepository.GetAsync(maNguoiDung);
        if (nguoiDung == null)
        {
            throw new NotFoundException(nguoiDung.GetTypeEx(), null);
        }

        await ValidationHelper.ValidateAsync(request, new NguoiDungModifyRequestValidation());

        // if (request.KichHoat == true) {
        // 	throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidStatus);
        // }

        nguoiDung = request.Adapt(nguoiDung);

        var response = await _nguoiDungRepository.UpdateAsync(nguoiDung);

        return response;
    }

    public async Task<int> ResetPasswordAsync(int maNguoiDung, string password)
    {
        var nguoiDung = await _nguoiDungRepository.GetAsync(maNguoiDung);
        if (nguoiDung == null)
        {
            throw new NotFoundException(nguoiDung.GetTypeEx(), null);
        }

        // account.MatKhau = BCrypt.Net.BCrypt.HashPassword(password);
        nguoiDung.MatKhau = GetPassword.GetMD5(nguoiDung.Salt + password);
        return await _nguoiDungRepository.UpdateAsync(nguoiDung);
    }

    public async Task<int> UpdatePassWordAsync(ModifiedUserPasswordRequest request)
    {
        var username = _authorizedContextFacade.Username;
        var nguoiDung = await _nguoiDungRepository.GetAsync(x => x.TenDangNhap == username);
        if (nguoiDung is null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.FirstTimeLogin);
        }

        var inputPasswordHash = GetPassword.GetMD5(nguoiDung.Salt + request.CurrentPassword);
        if (inputPasswordHash != nguoiDung.MatKhau)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidCurrentPassword);
        }

        nguoiDung.MatKhau = GetPassword.GetMD5(nguoiDung.Salt + request.NewPassword);
        
        return await _nguoiDungRepository.UpdateAsync(nguoiDung);
    }

    public async Task<int> CreateAsync(CreatedUserRequest userRequest)
    {
        await ValidationHelper.ValidateAsync(userRequest, new NguoiDungCreatedRequestValidation());
        var currentAccount = await _nguoiDungRepository.GetAsync(a => a.TenDangNhap == userRequest.TenDangNhap);
        if (currentAccount != null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.DuplicatedAccountName);
        }
        var currentPortal = await _donviDungRepository.GetAsync(x => x.MaDonViSuDung == userRequest.MaDonViSuDung && x.KichHoat == true);

        var nguoiDung = userRequest.Adapt<TbNguoiDung>();
        nguoiDung.Ho = "";
        nguoiDung.TenDangNhap = nguoiDung.TenDangNhap;
        nguoiDung.KichHoat = false;
        nguoiDung.Salt = GetPassword.GetRandomLetters(5);
        nguoiDung.MatKhau = GetPassword.GetMD5(nguoiDung.Salt + userRequest.MatKhau);
        nguoiDung.NgayTao = DateTime.UtcNow;
        nguoiDung.CoChoPhepHienThi = true;
        nguoiDung.MaKieuNguoiDung = 4223;
        nguoiDung.ThoiGianMatKhau = DateTime.Now;
        nguoiDung.MaTimeZone = currentPortal != null ? currentPortal.MaTimeZone : 0;
        return await _nguoiDungRepository.InsertAsync(nguoiDung);
    }
    
    //Lấy mã người dùng sau khi đăng nhập
    public async Task<TbNguoiDung> GetCurrentUserAsync()
    {
        var username = _authorizedContextFacade.Username;
        var currentUser = await _nguoiDungRepository.GetAsync(x => x.TenDangNhap == username);
        if (currentUser is null)
        {
            throw new BadRequestException(Constants.Exceptions.Messages.Login.FirstTimeLogin);
        }
        return currentUser;
    }
}