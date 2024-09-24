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
				public const string InvalidSupply = "Bạn chưa chọn vật tư";
				public const string EmptySupplyCode = "Mã vật tư không hợp lệ";
				public const string InvalidSupplyLocation = "Vị trí hiện tại của vật tư không đúng";
				public const string NoSupplyLocationSelected = "Bạn chưa chọn vị trí chi tiết vật tư";
				public const string InvalidFileType = "Loại file không được phép";
				public const string EmptySupplies = "Danh sách vật tư trống";
				public const string NoSupplyImageSelected = "Bạn chưa chọn ảnh vật tư";
				public const string SupplyNotExist = "Vật tư không còn trong hệ thống";
			}

			public static class Login {
				public const string DuplicatedAccountName = "Tên đăng nhập này đã tồn tại";
			}
			//Kỳ kiểm kê
			public static class InventoryCheck {
				public const string InvalidKyKiemKe = "Kỳ kiểm kê không hợp lệ";
				public const string InvalidKyKiemKeChiTiet = "Kỳ kiểm kê chi tiết không hợp lệ";
				public const string InvalidSoLuongKiemKe = "Số lượng kiểm kê không hợp lệ";
				public const string InvalidToTalMatVaKemPhamChat = "Tổng số lượng mất và kém phẩm chất không được vượt quá số lượng kiểm kê";
				public const string InvalidSoLuongUDong = "Số lượng ứ đọng không được vượt quá số lượng kiểm kê";
				public const string InvalidSoLuongDeNghiThanhLy = "Số lượng đề nghị thanh lý không được vượt quá số lượng kiểm kê";
				public const string NotFoundKyKiemKeChinh = "Hệ thống chưa chọn kỳ kiểm kê chính";
			}

			public static class SupplyTicket
			{
				public const string InvalidId = "Id phiếu cung ứng không hợp lệ";
				public const string InvalidSupplyTicket = "Bạn chưa chọn phiếu cung ứng";
			}

			public static class Cart
			{
				public const string InvalidQuantity = "Số lượng vật tư không được nhỏ hơn 1";
				public const string SupplyNotExist = "Vật tư không còn trong giỏ hàng";
				public const string DeletedSupply = "Vật tư đã không còn trong hệ thống nên đã bị xóa khỏi giỏ hàng";
			}
		}
	}
}
