using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class ModifiedSuppliesLocationRequestValidation : AbstractValidator<ModifiedSuppliesLocationRequest>
{
    public ModifiedSuppliesLocationRequestValidation()
    {
        RuleFor(x => x.IdToMay).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdGiaKe).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.IdNgan).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
        RuleFor(x => x.IdHop).GreaterThanOrEqualTo(1).WithMessage(MustGreaterThanOrEqual);
    }
}