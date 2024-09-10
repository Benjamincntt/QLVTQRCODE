using ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using ESPlatform.QRCode.IMS.Library.Utils.Validation;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.NguoiDungs;

public class NguoiDungModifyRequestValidation : AbstractValidator<ModifiedUserRequest> {
	public NguoiDungModifyRequestValidation() {
		RuleFor(o => o.FullName).ValidateStringDefault(false);
		RuleFor(x => x.Email)
			.ValidateStringDefault()
			.EmailAddress().WithMessage(Invalid);
		RuleFor(x => x.SoDienThoai)
			.ValidatePhoneNumber().WithMessage(Invalid);
	}
}