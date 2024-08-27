namespace ESPlatform.QRCode.IMS.Domain.ValueObjects;

public class RefreshTokenInfo {
	public string RefreshToken { get; set; } = null!;

	public DateTime ExpirationTime { get; set; }

	public DateTime CreatedTime { get; set; }

	public string CreatedByIpAddress { get; set; } = null!;

	public DateTime RevokedTime { get; set; } = DateTime.MinValue;

	public string RevokedByIpAddress { get; set; } = null!;

	public string SuccessorRefreshToken { get; set; } = string.Empty;

	public string RevokeReason { get; set; } = string.Empty;

	public bool IsExpired => DateTime.UtcNow >= ExpirationTime;

	public bool IsRevoked => RevokedTime > DateTime.MinValue;

	public bool IsActive => !IsRevoked && !IsExpired;
}
