using System.Security.Claims;
using ESPlatform.QRCode.IMS.Core.DTOs.Authentication;
using ESPlatform.QRCode.IMS.Core.DTOs.Authentication.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Authentication;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.ValueObjects;
using ESPlatform.QRCode.IMS.Infra.Builders;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Utils;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using Microsoft.Extensions.Caching.Distributed;

namespace ESPlatform.QRCode.IMS.Core.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IDonViSuDungRepository _donViSuDungRepository;
    private readonly INguoiDungRepository _nguoiDungRepository;
    private readonly IAuthorizedContextFacade _authorizedContextFacade;
    private readonly JwtFacade _jwtFacade;
    private readonly OtpFacade _otpFacade;
    private readonly IDistributedCache _distributedCache;

    public AuthenticationService(IAccountRepository accountRepository,
        IAuthorizedContextFacade authorizedContextFacade,
        IDonViSuDungRepository donViSuDungRepository,
        INguoiDungRepository nguoiDungRepository,
        JwtFacade jwtFacade,
        OtpFacade otpFacade,
        IDistributedCache distributedCache)
    {
        _accountRepository = accountRepository;
        _authorizedContextFacade = authorizedContextFacade;
        _donViSuDungRepository = donViSuDungRepository;
        _nguoiDungRepository = nguoiDungRepository;
        _jwtFacade = jwtFacade;
        _otpFacade = otpFacade;
        _distributedCache = distributedCache;
    }

    public async Task<LoginSuccessInfo> LoginAsync(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username))
        {
            throw new BadRequestException(Constants.Authentication.Messages.EmptyLoginName);
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new BadRequestException(Constants.Authentication.Messages.EmptyKeyword);
        }
        var nguoiDung = await GetEligibleAccountAsync(request.Username, true);
        if (nguoiDung is null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidLoginName);
        }
        
        var inputPasswordHash = GetPassword.GetMD5(nguoiDung.Salt + request.Password);
        if (inputPasswordHash != nguoiDung.MatKhau)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidPassword);
        }
        
        return await AcceptLogin(nguoiDung);
    }

    public async Task<LoginSuccessInfo> LoginVerifyAsync(LoginVerifyRequest request)
    {
        var nguoiDung = await GetEligibleAccountAsync(request.Username, true);
        if (nguoiDung is null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccount);
        }

        // var loginState = nguoiDung.Info.LoginState;
        // if (loginState is null
        //     || loginState.SecurityKey != request.SecurityKey
        //     || loginState.ExpirationTime < DateTime.UtcNow
        //     || loginState.IpAddress != _authorizedContextFacade.IpAddress)
        // {
        //     throw new BadRequestException(Constants.Authentication.Messages.InvalidSecurityKey);
        // }

        // if (!_otpFacade.VerifyOtp(request.Otp, nguoiDung.OtpSecret))
        // {
        //     throw new BadRequestException(Constants.Authentication.Messages.InvalidOtp);
        // }

        return await AcceptLogin(nguoiDung);
    }

    public async Task<LoginSuccessInfo> LoginWithoutOtpAsync(LoginRequest request)
    {
        if (AppConfig.Instance.OtpSettings.Enable)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidLoginMethod);
        }

        var nguoiDung = await GetEligibleAccountAsync(request.Username, true);
        if (nguoiDung == null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidUsernameOrPassword);
        }

        var inputPasswordHash = GetPassword.GetMD5(nguoiDung.Salt + request.Password);
        if (inputPasswordHash != nguoiDung.MatKhau)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidUsernameOrPassword);
        }

        return await AcceptLogin(nguoiDung);
    }

    public async Task<LoginSuccessInfo> RefreshAccessTokenAsync(LoginSuccessInfo loginInfo)
    {
        var principal = _jwtFacade.ValidateAccessToken(loginInfo.AccessToken);
        if (principal == null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(username))
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        var account = await GetEligibleAccountAsync(username, true);
        if (account == null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        // var currentRefreshTokenInfo =
        //     account.Info.RefreshTokenInfos?.SingleOrDefault(x => x.RefreshToken == loginInfo.RefreshToken);
        // if (currentRefreshTokenInfo is { IsRevoked: true })
        // {
        //     account.Info.RefreshTokenInfos = RevokeSuccessorRefreshTokens(currentRefreshTokenInfo,
        //         account.Info.RefreshTokenInfos!,
        //         Constants.Authentication.RevokeReasons.ReuseRevokedRefreshToken).ToList();
        //     await _nguoiDungRepository.UpdateAsync(account);
        // }

        // if (currentRefreshTokenInfo is not { IsActive: true })
        // {
        //     throw new BadRequestException(Constants.Authentication.Messages.InvalidRefreshToken);
        // }

        return await AcceptLogin(account);
    }

    public async Task RevokeRefreshTokenAsync(LoginSuccessInfo loginInfo)
    {
        var principal = _jwtFacade.ValidateAccessToken(loginInfo.AccessToken);
        if (principal == null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        var username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(username))
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        var account = await GetEligibleAccountAsync(username, true);
        if (account == null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
        }

        // var refreshToken =
        //     account.Info.RefreshTokenInfos?.SingleOrDefault(x => x.RefreshToken == loginInfo.RefreshToken);
        // if (refreshToken is not { IsActive: true })
        // {
        //     throw new BadRequestException(Constants.Authentication.Messages.InvalidRefreshToken);
        // }
        //
        // RevokeRefreshToken(refreshToken, Constants.Authentication.RevokeReasons.RevokeWithoutReplacement);
        await _nguoiDungRepository.UpdateAsync(account);
    }

    public async Task<bool> ActivateAccountAsync(ActivateAccountRequest request)
    {
        var account = await GetEligibleAccountAsync(request.Username, true);
        if (account is null)
        {
            throw new BadRequestException(Constants.Authentication.Messages.InvalidAccount);
        }

        // if (!_otpFacade.VerifyOtp(request.Otp, account.OtpSecret))
        // {
        //     throw new BadRequestException(Constants.Authentication.Messages.InvalidOtp);
        // }

        account.KichHoat = true;
        return await _nguoiDungRepository.UpdateAsync(account) > 0;
    }

    public async Task ClearAuthorizationCacheAsync(int accountId)
    {
        await _distributedCache.RemoveAsync(string.Format(Constants.CacheKeys.RoleDictionaryFormat, accountId));
        await _distributedCache.RemoveAsync(string.Format(Constants.CacheKeys.PermissionDictionaryFormat, accountId));
    }

    private async Task<TbNguoiDung?> GetEligibleAccountAsync(string username, bool kichHoat)
    {
        var predicate = AccountPredicateBuilder.Init()
            .HasUsername(username)
            .HasAnyStatus(kichHoat)
            .isDelete(false);
        var response = await _nguoiDungRepository.GetAsync(predicate);
        return response ?? null;
    }

    private async Task<LoginSuccessInfo>AcceptLogin(TbNguoiDung nguoiDung,
        RefreshTokenInfo? predecessorRefreshTokenInfo = null)
    {
        // otherClaims là các phần phụ như madonvisudung
        var otherClaims = new Dictionary<string, string>();
        var accessToken = _jwtFacade.GenerateAccessToken(nguoiDung.MaNguoiDung, nguoiDung.TenDangNhap, otherClaims);
        // var refreshTokenInfo = predecessorRefreshTokenInfo != null
        //     ? RotateRefreshToken(predecessorRefreshTokenInfo)
        //     : GenerateRefreshToken();

        // nguoiDung.Info.RefreshTokenInfos ??= new List<RefreshTokenInfo>();
        // nguoiDung.Info.RefreshTokenInfos = RemoveExpiredRefreshTokens(nguoiDung.Info.RefreshTokenInfos).ToList();
        // nguoiDung.Info.RefreshTokenInfos.Add(refreshTokenInfo);
        // nguoiDung.Info.LoginState = null;
        //
        // await _nguoiDungRepository.UpdateAsync(nguoiDung);

        await ClearAuthorizationCacheAsync(nguoiDung.MaNguoiDung);

        return new LoginSuccessInfo
        {
            AccessToken = accessToken,

            RefreshToken = MiscHelper.GenerateRandomBase64String(),
            
            TenNguoiDung = nguoiDung.Ten,
            
            ChucVu = nguoiDung.ChucVu ?? string.Empty,
            
            IdDonVi = nguoiDung.IddonVi,
            UserId = nguoiDung.MaNguoiDung
        };
    }

    private static IEnumerable<RefreshTokenInfo> RemoveExpiredRefreshTokens(IEnumerable<RefreshTokenInfo> refreshTokens)
    {
        return refreshTokens.Where(x =>
            x.IsActive || x.CreatedTime.AddMinutes(AppConfig.Instance.JwtSettings.RefreshTokenLifetimeMinutes) >
            DateTime.UtcNow).ToList();
    }

    private IEnumerable<RefreshTokenInfo> RevokeSuccessorRefreshTokens(RefreshTokenInfo refreshTokenInfo,
        IEnumerable<RefreshTokenInfo> refreshTokenInfoList,
        string reason)
    {
        var list = refreshTokenInfoList.ToList();
        if (string.IsNullOrEmpty(refreshTokenInfo.SuccessorRefreshToken))
        {
            return list;
        }

        var childTokenInfo = refreshTokenInfo;
        while (true)
        {
            childTokenInfo = list.SingleOrDefault(x => x.RefreshToken == childTokenInfo.SuccessorRefreshToken);
            if (childTokenInfo == null)
            {
                break;
            }

            if (childTokenInfo.IsActive)
            {
                RevokeRefreshToken(childTokenInfo, reason);
            }
        }

        return list;
    }

    private RefreshTokenInfo GenerateRefreshToken()
    {
        return new RefreshTokenInfo
        {
            RefreshToken = MiscHelper.GenerateRandomBase64String(),
            ExpirationTime = DateTime.UtcNow.AddMinutes(AppConfig.Instance.JwtSettings.RefreshTokenLifetimeMinutes),
            CreatedTime = DateTime.UtcNow,
            CreatedByIpAddress = _authorizedContextFacade.IpAddress
        };
    }

    private void RevokeRefreshToken(RefreshTokenInfo refreshTokenInfo, string reason = "",
        string successorRefreshToken = "")
    {
        refreshTokenInfo.RevokedTime = DateTime.UtcNow;
        refreshTokenInfo.RevokedByIpAddress = _authorizedContextFacade.IpAddress;
        refreshTokenInfo.RevokeReason = reason;
        refreshTokenInfo.SuccessorRefreshToken = successorRefreshToken;
    }

    private RefreshTokenInfo RotateRefreshToken(RefreshTokenInfo refreshTokenInfo)
    {
        var successor = GenerateRefreshToken();
        RevokeRefreshToken(refreshTokenInfo, Constants.Authentication.RevokeReasons.ReplaceByNewRefreshToken,
            successor.RefreshToken);
        return successor;
    }
}