using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class ModifiedSuppliesLocationRequestValidation : AbstractValidator<ModifiedSuppliesLocationRequest>
{
    public ModifiedSuppliesLocationRequestValidation()
    {
        RuleFor(x => x.IdToMay).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdGiaKe).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdNgan).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(x => x.IdHop).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
    }
}