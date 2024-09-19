using ESPlatform.QRCode.IMS.Library.Extensions;

namespace ESPlatform.QRCode.IMS.Library.Exceptions;

public class NotFoundException : ApplicationException {
	public NotFoundException(string? objectName, string? objectId) {
		if (objectId != null) {
			Message = objectName != null
				? $"Không tìm thấy {objectName} có Id là {objectId}"
				: $"Không tìm thấy đối tượng: {objectId}";
		}
		else if (objectName != null) {
			Message = $"Không tìm thấy đối tượng {objectName}";
		}
		else {
			Message = "Không tìm thấy tài nguyên yêu cầu";
		}
	}

	public NotFoundException(Type objectType, string? objectId) : this(objectType.GetDisplayName(), objectId) { }

	public NotFoundException(string message) {
		Message = message;
	}

	public override string Message { get; } = string.Empty;
}
