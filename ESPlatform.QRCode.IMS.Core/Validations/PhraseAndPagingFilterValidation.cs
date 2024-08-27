using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations;

public class PhraseAndPagingFilterValidation : AbstractValidator<PhraseAndPagingFilter> {
	public PhraseAndPagingFilterValidation() {
		RuleFor(x => x.Keywords).MaximumLength(StringDefaultMaximumLength).WithMessage(RequireMaximumLength);
		RuleFor(x => x.PageIndex).InclusiveBetween(MinPageSize, MaxPageSize).WithMessage(MustWithinValueRange);
		RuleFor(x => x.PageSize).InclusiveBetween(MinPageSize, MaxPageSize).WithMessage(MustWithinValueRange);
	}
}
