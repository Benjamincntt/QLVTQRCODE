using FluentValidation;
using FluentValidation.Validators;

namespace ESPlatform.QRCode.IMS.Core.Engine.Utils;

public static class ValidatorExtensions {
	public static IRuleBuilderOptions<T, TProperty> ValidateStringDefault<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, 
		bool allowNullOrEmpty = true) {
		var builder = ruleBuilder;

		if (!allowNullOrEmpty) {
			builder = builder.NotEmpty().WithMessage(Constants.Validation.Messages.MustNotEmpty);
		}

		return builder
			   .SetValidator((IPropertyValidator<T, TProperty>)new MaximumLengthValidator<T>(Constants.Validation.StringDefaultMaximumLength))
			   .WithMessage(Constants.Validation.Messages.RequireMaximumLength);
	}
}
