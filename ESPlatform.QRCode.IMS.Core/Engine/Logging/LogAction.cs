using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Core.Engine.Logging;

public class LogAction {
	public LogObjectType ObjectType { get; set; } = LogObjectType.Unknown;

	public string Action { get; set; } = string.Empty;

	public string Message { get; set; } = string.Empty;
}
