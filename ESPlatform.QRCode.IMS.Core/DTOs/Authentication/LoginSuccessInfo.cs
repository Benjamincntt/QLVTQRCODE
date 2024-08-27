namespace ESPlatform.QRCode.IMS.Core.DTOs.Authentication;

public class LoginSuccessInfo {
	public string AccessToken { get; set; } = string.Empty;

	public string RefreshToken { get; set; } = string.Empty;
}
