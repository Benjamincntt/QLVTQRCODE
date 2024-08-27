namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Authentication {
		public static class Messages {
			//public const string InsufficientPermissions = "Truy cập chưa được cấp quyền"; // 403 Forbidden
			public const string InvalidAccessToken = "Access Token không hợp lệ";
			public const string InvalidAccount = "Tài khoản không hợp lệ";
			public const string InvalidLoginMethod = "Phương thức đăng nhập không hợp lệ";
			//public const string InvalidOtp = "OTP không hợp lệ";
			public const string InvalidRefreshToken = "Refresh Token không hợp lệ";
			//public const string InvalidSecurityKey = "Security Key không hợp lệ";
			public const string InvalidUsernameOrPassword = "Tài khoản không hợp lệ";
			public const string RefreshTokenRevoked = "Refresh Token đã thu hồi";
			//public const string Unauthorized = "Truy cập chưa được xác thực"; // 401 Unauthorized
			public const string InvalidCurrentPassword = "Mật khẩu hiện tại không đúng";
		}

		public static class RevokeReasons {
			public const string ReplaceByNewRefreshToken = "Thay thế bởi Refresh Token mới";
			public const string ReuseRevokedRefreshToken = "Sử dụng Refresh Token đã thu hồi";
			public const string RevokeWithoutReplacement = "Thu hồi Refresh Token và không thay thế";
		}
	}
}
