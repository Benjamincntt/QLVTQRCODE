using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;

public class AccountModifyRequest {
	public string FullName { get; set; } = string.Empty;

	public string TenDangNhap { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;

	public string Ho { get; set; } = string.Empty;

	public string Ten { get; set; } = string.Empty;

	public string AnhDaiDien { get; set; } = string.Empty;

	public DateTime? NgaySinh { get; set; }

	public bool? GioiTinh { get; set; }

	public string HocHam { get; set; } = string.Empty;

	public string HocVi { get; set; } = string.Empty;

	public string DiaChi { get; set; } = string.Empty;

	public string NoiCongTac { get; set; } = string.Empty;
}