namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Responses;

public class AccountListResponseItem : AccountDto {
	public Guid AccountId { get; set; }
	
	public Guid AvatarFileId { get; set; }
}
