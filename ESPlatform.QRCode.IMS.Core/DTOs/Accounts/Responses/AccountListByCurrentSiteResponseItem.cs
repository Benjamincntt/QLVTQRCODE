namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Responses;

public class AccountListByCurrentSiteResponseItem {
	public Guid AccountId { get; set; }

	public string Username { get; set; } = string.Empty;

	public string FullName { get; set; } = string.Empty;
}
