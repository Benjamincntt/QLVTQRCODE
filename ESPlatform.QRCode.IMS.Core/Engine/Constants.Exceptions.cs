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
				public const string NotNullSiteId = "Mã trang không được để trống";
				public const string NotExistSiteId = "Mã trang không tồn tại";
			}

			public static class Content {
				public const string InvalidContentId = "Mã bài viết không được để trống";
				public const string PrimaryCategoryIsRequiredAndOnlyOne = "Tin bài phải có duy nhất một chuyên mục chính";
			}

			public static class Royalty {
				public const string InvalidRoyaltyPriceId = "Mã đơn giá không được để trống";
				public const string InvalidRoyaltyFactorId = "Mã hệ số nhuận bút không được để trống";
				public const string DuplicatedRoyaltyPrice = "Đơn giá này đã tồn tại";
				public const string DuplicatedRoyaltyFactor = "Hệ số nhuận bút này đã tồn tại";
				public const string OperationUnauthorized = "Không có quyền thao tác Nhuận bút";
				public const string ApprovedRoyaltyCannotBeChanged = "Nhuận bút đã phê duyệt không thể thay đổi";
				public const string InvalidRoyaltyStatus = "Trạng thái Nhuận bút không hợp lệ";
				public const string TotalSplitSharesCannotExceed100Pc = "Tổng tỷ lệ đóng góp không được vượt quá 100%";
			}

			public static class Label {
				public const string InvalidLabelId = "Mã nhãn không được để trống";
				public const string DuplicatedLabel = "Nhãn này đã tồn tại";
			}

			public static class Workflow {
				public const string ScopeServiceNotFound = "Không tìm thấy dịch vụ phân quyền";
				public const string AssignedAccountNotFound = "Không tìm thấy tài khoản đích";
				public const string CategoryNotFound = "Tin bài chưa có chuyên mục";
				public const string ContentNotFound = "Không tìm thấy tin bài";
				public const string OperationUnauthorized = "Không có quyền thao tác tin bài";
				public const string OperationUnauthorizedOnOneOfCategories = "Không có quyền thao tác tin bài ở một trong các chuyên mục đã chọn";
				public const string OperationUnauthorizedOnPrimaryCategory = "Không có quyền thao tác tin bài trong chuyên mục chính";
				public const string PrimaryCategoryNotFoundOrNotUnique = "Chuyên mục chính chưa có hoặc không phải duy nhất";
				public const string StatusIsInappropriate = "Trạng thái tin bài không phù hợp";
			}

			public static class Management {
				public const string OperationOnSelectedCategoryUnauthorized = "Không có quyền thao tác tin bài trong chuyên mục chính";
			}

			public static class Login {
				public const string NotNullAccountName = "Tên đăng nhập không được để trống";
				public const string NotExistAccountName = "Tên đăng nhập không không tồn tại";
				public const string DuplicatedAccountName = "Tên đăng nhập này đã tồn tại";
			}
		}
	}
}
