using ESPlatform.QRCode.IMS.Library.Exceptions;
using FluentValidation;

namespace ESPlatform.QRCode.IMS.Library.Utils.Validation;

public static class ValidationHelper {
	public static async Task ValidateAsync<T, TValidator>(T t, TValidator validator) where T : new() where TValidator : AbstractValidator<T> {
		var validationResult = await validator.ValidateAsync(t);

		if (validationResult.Errors.Any()) {
			throw new FluentValidationException(validationResult);
		}
	}

	public static void Validate<T, TValidator>(T request, TValidator validator) where T : new() where TValidator : AbstractValidator<T> {
		var validationResult = validator.Validate(request);

		if (validationResult.Errors.Any()) {
			throw new FluentValidationException(validationResult);
		}
	}
}
