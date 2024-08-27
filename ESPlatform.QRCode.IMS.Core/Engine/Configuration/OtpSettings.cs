namespace ESPlatform.QRCode.IMS.Core.Engine.Configuration;

public class OtpSettings {
	public string Issuer { get; set; } = null!;

	public int Period { get; set; } = 30;

	public int Digits { get; set; } = 6;

	public bool Enable { get; set; } = false;
}
