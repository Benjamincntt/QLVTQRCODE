using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class CreatedSupplyRequestValidation : AbstractValidator<CreatedSupplyRequest>
{
    public CreatedSupplyRequestValidation()
    {
        RuleFor(x => x.TenVatTu).ValidateStringDefault(false)
            .MaximumLength(500).WithMessage(RequireMaximumLength);
        RuleFor(x => x.DonViTinh).ValidateStringDefault()
            .MaximumLength(100).WithMessage(RequireMaximumLength);
        RuleFor(x => x.GhiChu).ValidateStringDefault()
            .MaximumLength(250).WithMessage(RequireMaximumLength);
        RuleFor(x => x.ThongSoKyThuat).ValidateStringDefault();
    }
}