namespace ESPlatform.QRCode.IMS.Library.Exceptions;

public class BadRequestException : ApplicationException, IMultiErrorsException {
	public override string Message { get; } = string.Empty;

	public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();

	public BadRequestException(string message) {
		Message = message;
	}

	public BadRequestException(string message, IEnumerable<string> errors) {
		Message = message;
		Errors = errors;
	}
}
