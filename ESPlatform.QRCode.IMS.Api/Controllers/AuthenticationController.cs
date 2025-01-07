// using ESPlatform.QRCode.IMS.Api.Controllers.Base;
// using ESPlatform.QRCode.IMS.Core.DTOs.Authentication;
// using ESPlatform.QRCode.IMS.Core.DTOs.Authentication.Requests;
// using ESPlatform.QRCode.IMS.Core.Engine;
// using ESPlatform.QRCode.IMS.Core.Services.Authentication;
// using ESPlatform.QRCode.IMS.Library.Exceptions;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace ESPlatform.QRCode.IMS.Api.Controllers;
//
// public class AuthenticationController : ApiControllerBase {
// 	private readonly IAuthenticationService _authenticationService;
//
// 	public AuthenticationController(IAuthenticationService authenticationService) {
// 		_authenticationService = authenticationService;
// 	}
// 	
// 	/// <summary>
// 	/// Đăng nhập
// 	/// </summary>
// 	/// <param name="request"></param>
// 	/// <returns></returns>
// 	[HttpPost("login")]
// 	[AllowAnonymous]
// 	public async Task<LoginSuccessInfo> LoginAsync([FromBody] LoginRequest request) {
// 		return await _authenticationService.LoginAsync(request);
// 	}
//
// 	// [HttpPost("login-verify")]
// 	// [AllowAnonymous]
// 	// public async Task<LoginSuccessInfo> LoginVerifyAsync([FromBody] LoginVerifyRequest request) {
// 	// 	return await _authenticationService.LoginVerifyAsync(request);
// 	// }
// 	//
// 	
// 	/// <summary>
// 	/// Làm mới access token
// 	/// </summary>
// 	/// <param name="loginInfo"></param>
// 	/// <returns></returns>
// 	/// <exception cref="BadRequestException"></exception>
// 	[HttpPost("refresh-access-token")]
// 	[AllowAnonymous]
// 	public async Task<LoginSuccessInfo> RefreshAccessTokenAsync([FromBody] LoginSuccessInfo loginInfo) {
// 		if (string.IsNullOrEmpty(loginInfo.AccessToken)) {
// 			loginInfo.AccessToken = GetAccessToken();
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.RefreshToken)) {
// 			loginInfo.RefreshToken = GetRefreshTokenCookie();
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.AccessToken)) {
// 			throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.RefreshToken)) {
// 			throw new BadRequestException(Constants.Authentication.Messages.InvalidRefreshToken);
// 		}
// 	
// 		var response = await _authenticationService.RefreshAccessTokenAsync(loginInfo);
// 		if (response is null) {
// 			throw new BadRequestException(Constants.Authentication.Messages.InvalidRefreshToken);
// 		}
// 	
// 		SetRefreshTokenCookie(response.RefreshToken);
// 		return response;
// 	}
// 	
// 	/// <summary>
// 	/// Thu hồi access token
// 	/// </summary>
// 	/// <param name="loginInfo"></param>
// 	/// <returns></returns>
// 	/// <exception cref="BadRequestException"></exception>
// 	[HttpPost("revoke-refresh-token")]
// 	public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody] LoginSuccessInfo loginInfo) {
// 		if (string.IsNullOrEmpty(loginInfo.AccessToken)) {
// 			loginInfo.AccessToken = GetAccessToken();
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.RefreshToken)) {
// 			loginInfo.RefreshToken = GetRefreshTokenCookie();
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.AccessToken)) {
// 			throw new BadRequestException(Constants.Authentication.Messages.InvalidAccessToken);
// 		}
// 	
// 		if (string.IsNullOrEmpty(loginInfo.RefreshToken)) {
// 			throw new BadRequestException(Constants.Authentication.Messages.InvalidRefreshToken);
// 		}
// 	
// 		await _authenticationService.RevokeRefreshTokenAsync(loginInfo);
// 		return Ok(new { Message = Constants.Authentication.Messages.RefreshTokenRevoked });
// 	}
// 	
// 	// /// <summary>
// 	// /// Kích hoạt tài khoản
// 	// /// </summary>
// 	// /// <param name="request"></param>
// 	// /// <returns></returns>
// 	// [HttpPost("activate-account")]
// 	// [AllowAnonymous]
// 	// public async Task<bool> ActivateAccountAsync([FromBody] ActivateAccountRequest request) {
// 	// 	return await _authenticationService.ActivateAccountAsync(request);
// 	// }
// 	//
// 	// [HttpDelete("clear-cache")]
// 	// [AllowAnonymous]
// 	// public async Task ClearCacheAsync([FromQuery, Required] Guid accountId) {
// 	// 	await _authenticationService.ClearAuthorizationCacheAsync(accountId);
// 	// }
// }
