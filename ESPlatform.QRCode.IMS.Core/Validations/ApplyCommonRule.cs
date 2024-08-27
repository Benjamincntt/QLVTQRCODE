// using System.Linq.Expressions;
// using FluentValidation;
// using FluentValidation.Validators;
// using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
// using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;
//
// namespace ESPlatform.QRCode.IMS.Core.Validations;
//
// public static class ValidationFilterHelper {
// 	internal class ComparableComparer<T> : IComparer<T> where T : IComparable<T> {
// 		internal static ComparableComparer<T> Instance { get; }
//
// 		static ComparableComparer() {
// 			Instance = new ComparableComparer<T>();
// 		}
//
// 		public int Compare(T x, T y) {
// 			return x.CompareTo(y);
// 		}
// 	}
// 	public static class RangeValidatorFactory {
// 		public static ExclusiveBetweenValidator<T, TProperty> CreateExclusiveBetween<T,TProperty>(TProperty from, TProperty to)
// 			where TProperty : IComparable<TProperty>, IComparable =>
// 			new ExclusiveBetweenValidator<T, TProperty>(from, to, ComparableComparer<TProperty>.Instance);
//
// 		public static InclusiveBetweenValidator<T, TProperty> CreateInclusiveBetween<T,TProperty>(TProperty from, TProperty to)
// 			where TProperty : IComparable<TProperty>, IComparable {
// 			return new InclusiveBetweenValidator<T, TProperty>(from, to, ComparableComparer<TProperty>.Instance);
// 		}
// 	}
// 	public static IRuleBuilderOptions<T, TProperty> InclusiveBetween<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty from, TProperty to) where TProperty : IComparable<TProperty>, IComparable {
// 		return ruleBuilder.SetValidator(RangeValidatorFactory.CreateInclusiveBetween<T,TProperty>(from, to));
// 	}
// 	public static void ApplyCommonRuleFor<T, TProperty>(AbstractValidator<T> validator, Expression<Func<T, TProperty>> propertyExpression) {
// 		validator.RuleFor(propertyExpression).Custom((value, context) => {
// 			if (value == null) {
// 				return;
// 			}
//
// 			if (typeof(TProperty) == typeof(int)) {
// 				var intValue = Convert.ToInt32(value);
// 				InclusiveBetween(intValue,MinPageSize, MaxPageSize).WithMessage(MustWithinValueRange);
// 			}
//
// 			// if (typeof(TProperty) == typeof(string)) {
// 			// 	var stringValue = value.ToString();
// 			// 	if (stringValue is { Length: < MinPageSize or > MaxPageSize }) {
// 			// 		context.AddFailure(MustWithinValueRange);
// 			// 	}
// 			// }
// 			// else if (typeof(TProperty) == typeof(int)) {
// 			// 	var intValue = Convert.ToInt32(value);
// 			// 	if (intValue < MinPageSize || intValue > MaxPageSize) {
// 			// 		context.AddFailure(MustWithinValueRange);
// 			// 	}
// 			// }
// 		});
// 	}
// }
