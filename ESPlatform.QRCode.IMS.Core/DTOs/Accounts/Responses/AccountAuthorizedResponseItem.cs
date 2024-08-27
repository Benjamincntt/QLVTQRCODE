namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Responses;

public class AccountAuthorizedResponseItem {
	public Guid AccountId { get; set; }

	public string Username { get; set; } = null!;

	public string FullName { get; set; } = null!;
}
