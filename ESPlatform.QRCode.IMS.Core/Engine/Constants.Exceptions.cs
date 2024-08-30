namespace ESPlatform.QRCode.IMS.Core.Engine;

public static partial class Constants {
	public static class Exceptions {
		public static class Messages {
			public static class Common {
				public const string DataExists = "Dữ liệu đã tồn tại";
				public const string Duplicated = "Dữ liệu bị trùng lặp";
				public const string InsertFailed = "Thêm không thành công";
				public const string InvalidApiParameters = "Tham số đầu vào của API không hợp lệ";
				public const string InvalidParameters = "Tham số không hợp lệ";
				public const string InvalidStatus = "Không thể sửa trạng thái thành Chờ kích hoạt.";
				public const string Unauthorized = "Truy cập chưa được xác thực";
				public const string UpdateFailed = "Cập nhật không thành công";
				public const string InvalidPassword = "Mật khẩu cũ không đúng";
				public const string UrlSlugDuplicated = "Đường dẫn bị trùng lặp";
				public const string NotExistSiteId = "Mã trang không tồn tại";
			}
			
			public static class Supplies {
				public const string InvalidId = "Mã vật tư không hợp lệ";
			}

			public static class Login {
				public const string DuplicatedAccountName = "Tên đăng nhập này đã tồn tại";
			}
		}
	}
}
