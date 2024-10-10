namespace ESPlatform.QRCode.IMS.Core.DTOs.Authentication;

public class LoginSuccessInfo {
	public string AccessToken { get; set; } = string.Empty;

	public string RefreshToken { get; set; } = string.Empty;
	
	public string TenNguoiDung { get; set; } = string.Empty;
	
	public string ChucVu { get; set; } = string.Empty;

	public int? IdDonVi { get; set; } = 0;
	public int UserId { get;set; }
}
