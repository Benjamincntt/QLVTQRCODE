using FluentValidation;
using FluentValidation.Validators;

namespace ESPlatform.QRCode.IMS.Library.Utils.Validation;

public static class ValidationExtensions {
	public static IRuleBuilderOptions<T, TProperty> ValidatePhoneNumber<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) {
		return ruleBuilder.SetValidator((IPropertyValidator<T, TProperty>)new RegularExpressionValidator<T>(@"^\+?[0-9]{10,12}$"));
	}
}
