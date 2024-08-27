using ESPlatform.QRCode.IMS.Core.DTOs.Authentication;
using ESPlatform.QRCode.IMS.Core.DTOs.Authentication.Requests;

namespace ESPlatform.QRCode.IMS.Core.Services.Authentication;

public interface IAuthenticationService {
	Task<LoginSuccessInfo> LoginAsync(LoginRequest request);

	Task<LoginSuccessInfo> LoginVerifyAsync(LoginVerifyRequest request);

	Task<LoginSuccessInfo> LoginWithoutOtpAsync(LoginRequest request);

	Task<LoginSuccessInfo> RefreshAccessTokenAsync(LoginSuccessInfo loginInfo);

	Task RevokeRefreshTokenAsync(LoginSuccessInfo loginInfo);

	Task<bool> ActivateAccountAsync(ActivateAccountRequest request);

	Task ClearAuthorizationCacheAsync(int accountId);
}
