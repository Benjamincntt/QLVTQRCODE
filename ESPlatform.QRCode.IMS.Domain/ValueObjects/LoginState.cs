namespace ESPlatform.QRCode.IMS.Domain.ValueObjects;

public class LoginState {
	public string SecurityKey { get; set; } = null!;

	public DateTime ExpirationTime { get; set; }

	public string IpAddress { get; set; } = null!;
}
