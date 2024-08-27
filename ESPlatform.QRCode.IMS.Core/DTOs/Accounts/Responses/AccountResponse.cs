namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Responses;

public class AccountResponse : AccountDto {
	public Guid AvatarFileId { get; set; }
	
	public string AvatarUrl { get; set; } = string.Empty;

	public string Title { get; set; } = string.Empty;
	
	public Guid DepartmentLabelId { get; set; }
	
	public Guid GroupLabelId { get; set; }

	public string DepartmentName { get; set; } = string.Empty;

	public string GroupName { get; set; } = string.Empty;
}
