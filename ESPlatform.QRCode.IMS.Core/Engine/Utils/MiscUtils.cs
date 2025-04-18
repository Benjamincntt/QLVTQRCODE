using System.Security.Claims;
using ESPlatform.QRCode.IMS.Library.Extensions;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Engine.Utils;

public static class MiscUtils {
	public static int GetKyKiemKeId(this HttpContext context) {
		return int.TryParse(context.Request.Headers[Constants.Http.HeaderNames.KiKiemKeId].FirstOrDefault(), out var id) ? id : Constants.KyKiemKeId;
	}

	public static int GetAccountId(this HttpContext context) {
		return context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToInt() ?? 0;
	}

	public static string GetUsername(this HttpContext context) {
		return context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
	}
    
}
