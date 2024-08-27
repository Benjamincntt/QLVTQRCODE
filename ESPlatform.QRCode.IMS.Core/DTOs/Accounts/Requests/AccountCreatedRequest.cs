using System.ComponentModel;

namespace ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;

public class AccountCreatedRequest : AccountDto {
	public int LoginFailedCount = 0;

	[DisplayName("Mật khẩu")]
	public string Password { get; set; } = string.Empty;

	public Guid AvatarFileId { get; set; } = Guid.Empty;

	public DateTime LoginFailedObservationTime => DateTime.UtcNow;

	public DateTime CreatedTime { get; set; }

	public string Title { get; set; } = string.Empty;

	public Guid DepartmentLabelId { get; set; }

	public Guid GroupLabelId { get; set; }
}