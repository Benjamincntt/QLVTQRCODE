namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;

public class AccountUpdatePasswordRequest {
	public string CurrentPassword { get; set; } = string.Empty;

	public string NewPassword { get; set; } = string.Empty;
}