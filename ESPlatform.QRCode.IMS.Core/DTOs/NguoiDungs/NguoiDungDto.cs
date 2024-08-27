namespace ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs;

public class NguoiDungDto {
	public string FullName { get; set; } = string.Empty;

	public string TenDangNhap { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	public DateTime? NgaySinh { get; set; }

	public string DiaChi { get; set; } = string.Empty;
}