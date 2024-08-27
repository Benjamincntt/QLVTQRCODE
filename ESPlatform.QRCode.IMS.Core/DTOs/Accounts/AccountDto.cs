using System.ComponentModel;
using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts;

public class AccountDto {
	[DisplayName("Tên tài khoản")]
	public string Username { get; set; } = string.Empty;

	[DisplayName("Tên đầy đủ")]
	public string FullName { get; set; } = string.Empty;

	public string Email { get; set; } = string.Empty;

	[DisplayName("Số điện thoại")]
	public string PhoneNumber { get; set; } = string.Empty;

	public bool IsLocked { get; set; } = false;

	//public AccountStatus Status { get; set; }
}