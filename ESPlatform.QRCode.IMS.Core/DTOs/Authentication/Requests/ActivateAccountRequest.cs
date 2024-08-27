namespace ESPlatform.QRCode.IMS.Core.DTOs.Authentication.Requests;

public class ActivateAccountRequest {
	public string Username { get; set; } = string.Empty;

	public string Otp { get; set; } = string.Empty;
}
