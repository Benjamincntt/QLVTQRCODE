using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations;

public class PagingFilterValidation  : AbstractValidator<PagingFilter> {
	public PagingFilterValidation() {
		RuleFor(o => o.PageIndex).InclusiveBetween(MinPageSize, MaxPageSize).WithMessage(MustWithinValueRange);
		RuleFor(o => o.PageSize).InclusiveBetween(MinPageSize, MaxPageSize).WithMessage(MustWithinValueRange);
	}
}
