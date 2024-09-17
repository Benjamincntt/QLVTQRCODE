using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class SupplyListRequestValidation: AbstractValidator<SupplyListRequest>
{
    public SupplyListRequestValidation()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.PageSize).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdViTri).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdKho).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(x => x.TenVatTu).ValidateStringDefault();
        RuleFor(x => x.MaVatTu).ValidateStringDefault();
    }
}