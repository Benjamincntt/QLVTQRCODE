using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class SupplyTicketDetailRequestValidation : AbstractValidator<SupplyTicketDetailRequest>
{
    public SupplyTicketDetailRequestValidation()
    {
        RuleFor(x => x.TenVatTu).ValidateStringDefault(false)
            .MaximumLength(StringDefaultMaximumLength).WithMessage(RequireMaximumLength);
        RuleFor(x => x.DonViTinh).ValidateStringDefault();
        RuleFor(x => x.GhiChu).ValidateStringDefault();
        RuleFor(x => x.ThongSoKyThuat).ValidateStringDefault();
        RuleFor(x => x.SoLuong).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
        RuleFor(x => x.VatTuId).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
        
    }
}