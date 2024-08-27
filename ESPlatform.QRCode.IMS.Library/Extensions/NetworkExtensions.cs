using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class NetworkExtensions {
	private const string XForwardedForHeaderName = "X-Forwarded-For";

	public static string GetClientIpAddress(this HttpContext? context) {
		if (context == null) {
			return string.Empty;
		}

		var ip = context.Request.Headers.TryGetValue(XForwardedForHeaderName, out var ips)
					 ? ips.First()?.Split(',', StringSplitOptions.RemoveEmptyEntries).First()
					 : string.Empty;

		if (string.IsNullOrEmpty(ip)) {
			ip = context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? string.Empty;
		}

		return ip;
	}
}
