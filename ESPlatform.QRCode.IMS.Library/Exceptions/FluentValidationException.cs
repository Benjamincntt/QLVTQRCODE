using System.ComponentModel.DataAnnotations;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ESPlatform.QRCode.IMS.Library.Exceptions;

public class FluentValidationException : ValidationException, IMultiErrorsException {
	public FluentValidationException(string message, ValidationResult validationResult) {
		Message = message;
		Errors = validationResult.Errors.Select(validationError => validationError.ErrorMessage).ToList();
	}

	public FluentValidationException(ValidationResult validationResult) : this("Dữ liệu không hợp lệ", validationResult) { }

	public override string Message { get; } = string.Empty;

	public IEnumerable<string> Errors { get; set; }
}
