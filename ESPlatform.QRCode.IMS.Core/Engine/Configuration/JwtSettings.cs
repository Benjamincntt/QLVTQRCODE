namespace ESPlatform.QRCode.IMS.Core.Engine.Configuration;

public class JwtSettings {
	public string Issuer { get; set; } = null!;

	public string Audience { get; set; } = null!;

	public string Key { get; set; } = null!;

	public int AccessTokenLifetimeMinutes { get; set; }

	public int RefreshTokenLifetimeMinutes { get; set; }
}
