namespace ESPlatform.QRCode.IMS.Core.DTOs.Logs;

public class ObjectLogModel {
	public Guid LogId { get; set; } = Guid.Empty;

	public string Action { get; set; } = string.Empty;

	public string Message { get; set; } = string.Empty;

	public DateTime Time { get; set; } = DateTime.Now;

	public Guid AccountId { get; set; }

	public string FullName { get; set; } = null!;
}
