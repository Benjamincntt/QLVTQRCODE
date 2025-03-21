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
				public const string EmptySupplyCode = "Mã vật tư bạn nhập vào không đúng. Vui lòng kiểm tra lại và thử lại";
				public const string InvalidSupplyLocation = "Vị trí hiện tại của vật tư không đúng";
				public const string NoSupplyLocationSelected = "Bạn chưa chọn vị trí chi tiết vật tư";
				public const string InvalidFileType = "Loại file không được phép";
				public const string EmptySupplies = "Danh sách vật tư trống";
				public const string NoSupplyImageSelected = "Bạn chưa chọn ảnh vật tư";
				public const string SupplyNotExist = "Vật tư không còn trong hệ thống";
				public const string FailedToInsertSupply = "Thêm vật tư không thành công";
				public const string DuplicatedSupplyName = "Vật tư có cùng tên đã tồn tại";
                public const string InvalidOrganization = "Bạn chưa chọn kho";
			}

			public static class Login {
				public const string DuplicatedAccountName = "Tên đăng nhập này đã tồn tại";

                public const string FirstTimeLogin =
                    "Tài khoản của bạn chưa được đồng bộ trên hệ thống kiểm kê vật tư qua QRCode. Vui lòng đăng nhập trên trang website của hệ thống trước";
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
				public const string InvalidSupplyTicket = "Bạn chưa chọn phiếu cung ứng";
                public const string InvalidSupplyTicketStatus = "Trạng thái phiếu cung ứng không hợp lệ";
                public const string TicketCancelSuccess  = "Hủy phiếu thành công";
                public const string TicketCancelFailed  = "Hủy phiếu thất bại";
			}

			public static class Cart
			{
				public const string InvalidQuantity = "Số lượng vật tư không được nhỏ hơn 1";
				public const string SupplyNotExist = "Vật tư không còn trong giỏ hàng";
				public const string DeletedSupply = "Vật tư đã không còn trong hệ thống nên đã bị xóa khỏi giỏ hàng";
				public const string InvalidCartInfo = "Thông tin giỏ hàng không chính xác";
			}
            // ký phiếu cung ứng
            public static class KyCungUng
            {
                public const string EmptyPhieuIds = "Không có phiếu nào";
                public const string InvalidPdx = "Không tồn tại phiếu đề xuất";
                public const string InvalidChuKy = "Cấu hình chữ ký không tồn tại";
                public const string Signed = "Phiếu đã được ký";
                public const string CanNotIgnore = "Người ký không được bỏ qua chữ ký này";
                public const string NotFoundCauHinhVanbanKy = "Người dùng chưa được cấu hình ký văn bản nào";
                public const string InvalidSupply = "Không có vật tư nào được chọn trong phiếu này";
                public const string NotFoundSignInfo = "Không có thông tin ký cho phiếu này";
                public const string CancelSignFailedMessage  = "Huỷ ký không thành công";
                public const string UpdateTicketStatusFailed = "Cập nhật trạng thái phiếu không thành công";
                public const string UpdateVanBanKyStatusFailed = "Cập nhật trạng thái văn bản ký không thành công";
                public const int DaKy = 1;
				public const int HuyBo = -1;
                public const int BoQua = 2;
                public const string InvalidTicketId = "Mã phiếu không hợp lệ";
            }
			
        }
	}
}
