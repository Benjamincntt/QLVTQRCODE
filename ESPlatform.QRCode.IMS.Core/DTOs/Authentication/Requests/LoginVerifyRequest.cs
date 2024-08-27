namespace ESPlatform.QRCode.IMS.Core.DTOs.Authentication.Requests;

public class LoginVerifyRequest {
	public string Username { get; set; } = string.Empty;

	public string SecurityKey { get; set; } = string.Empty;

	public string Otp { get; set; } = string.Empty;
}
