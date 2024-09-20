namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Authentication {
		public static class Messages {
			public const string InvalidAccessToken = "Access Token không hợp lệ";
			public const string InvalidAccount = "Tài khoản không hợp lệ";
			public const string InvalidLoginMethod = "Phương thức đăng nhập không hợp lệ";
			public const string InvalidRefreshToken = "Refresh Token không hợp lệ";
			public const string InvalidUsernameOrPassword = "Tài khoản không hợp lệ";
			public const string RefreshTokenRevoked = "Refresh Token đã thu hồi";
			public const string InvalidCurrentPassword = "Mật khẩu hiện tại không đúng";
			public const string InvalidPassword = "Nhập sai mật khẩu người dùng";
			public const string InvalidLoginName = "Tên đăng nhập không đúng";
			public const string EmptyLoginName = "Bạn chưa điền tên đăng nhập";
			public const string EmptyKeyword = "Bạn chưa điền mật khẩu";
		}

		public static class RevokeReasons {
			public const string ReplaceByNewRefreshToken = "Thay thế bởi Refresh Token mới";
			public const string ReuseRevokedRefreshToken = "Sử dụng Refresh Token đã thu hồi";
			public const string RevokeWithoutReplacement = "Thu hồi Refresh Token và không thay thế";
		}
	}
}
