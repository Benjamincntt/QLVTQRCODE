using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using ESPlatform.QRCode.IMS.Library.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace ESPlatform.QRCode.IMS.Api.Controllers.Base;

[ApiController]
[Route("/api/v1/[controller]")]
[Authorize]
public class ApiControllerBase : ControllerBase {
	private int _currentAccountId = 0;
	private string _currentIpAddress = string.Empty;
	private Guid _currentSiteId = Guid.Empty;
	private string _currentUsername = string.Empty;

	protected int CurrentAccountId => _currentAccountId == 0 ? _currentAccountId = HttpContext.GetAccountId() : _currentAccountId;

	protected string CurrentUsername => string.IsNullOrEmpty(_currentUsername) ? _currentUsername = HttpContext.GetUsername() : _currentUsername;

	protected string CurrentIpAddress => string.IsNullOrEmpty(_currentIpAddress) ? _currentIpAddress = HttpContext.GetClientIpAddress() : _currentIpAddress;

	protected string GetAccessToken() {
		var token = Request.Headers[HeaderNames.Authorization].ToString();
		if (string.IsNullOrEmpty(token)) {
			token = Request.Cookies[Constants.Http.CookieNames.AccessToken];
		}

		return !string.IsNullOrEmpty(token) ? token.StartsWith("Bearer ") ? token[7..] : token : string.Empty;
	}

	protected void SetRefreshTokenCookie(string token) {
		var cookieOptions = new CookieOptions {
			HttpOnly = true,
			Expires = DateTime.UtcNow.AddMinutes(AppConfig.Instance.JwtSettings.RefreshTokenLifetimeMinutes)
		};
		Response.Cookies.Append(Constants.Http.CookieNames.RefreshToken, token, cookieOptions);
	}

	protected string GetRefreshTokenCookie() {
		return Request.Cookies[Constants.Http.CookieNames.RefreshToken] ?? string.Empty;
	}
}
