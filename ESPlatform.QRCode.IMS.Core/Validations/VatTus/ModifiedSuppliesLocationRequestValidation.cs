using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using FluentValidation;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class ModifiedSuppliesLocationRequestValidation : AbstractValidator<ModifiedSuppliesLocationRequest>
{
    public ModifiedSuppliesLocationRequestValidation()
    {
        RuleFor(x => x.IdToMay).GreaterThanOrEqualTo(0);
        RuleFor(o => o.IdGiaKe).GreaterThanOrEqualTo(0);
        RuleFor(o => o.IdNgan).GreaterThanOrEqualTo(0);
        RuleFor(x => x.IdHop).GreaterThanOrEqualTo(0);
    }
}