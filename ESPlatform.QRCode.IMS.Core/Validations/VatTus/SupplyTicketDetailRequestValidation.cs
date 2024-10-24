using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class SupplyTicketDetailRequestValidation : AbstractValidator<SupplyTicketDetailRequest>
{
    public SupplyTicketDetailRequestValidation()
    {
        RuleFor(x => x.TenVatTu).ValidateStringDefault(false)
            .MaximumLength(500).WithMessage(RequireMaximumLength);
        RuleFor(x => x.DonViTinh).ValidateStringDefault();
        RuleFor(x => x.GhiChu).ValidateStringDefault()
            .MaximumLength(250).WithMessage(RequireMaximumLength);
        RuleFor(x => x.ThongSoKyThuat).ValidateStringDefault();
        RuleFor(x => x.SoLuong).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
    }
}