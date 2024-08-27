namespace ESPlatform.QRCode.IMS.Domain.ValueObjects;

public class AccountInfo {
	public List<RefreshTokenInfo>? RefreshTokenInfos { get; set; }

	public LoginState? LoginState { get; set; }
}
