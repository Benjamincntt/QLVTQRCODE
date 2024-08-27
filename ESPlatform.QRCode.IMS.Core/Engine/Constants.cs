namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public const int DefaultPageIndex = 1;
	public const int DefaultPageSize = 20;

	public static class Http {
		public const string CorsPolicyName = "CorsPolicyOrigins";

		public static class CookieNames {
			public const string AccessToken = "access_token";
			public const string RefreshToken = "refresh_token";
		}

		public static class HeaderNames {
			public const string SiteId = "Site-Id";
			public const string TokenExpired = "Token-Expired";
		}
	}

	public static class ContextKeys {
		public const string SiteId = "SiteId";
		public const string AccountId = "AccountId";
		public const string Username = "Username";
		public const string IpAddress = "IpAddress";
		public const string RoleDictionary = "RoleDictionary";
		public const string PermissionDictionary = "PermissionDictionary";
	}

	public static class CacheKeys {
		public const string RoleDictionaryFormat = "RoleDictionary_{0}";
		public const string PermissionDictionaryFormat = "PermissionDictionary_{0}";
	}
}
