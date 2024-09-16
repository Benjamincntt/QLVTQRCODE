using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using FluentValidation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation;
using static ESPlatform.QRCode.IMS.Core.Engine.Constants.Validation.Messages;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class ModifiedSuppliesDffRequestValidation: AbstractValidator<ModifiedSuppliesDffRequest>
{
    public ModifiedSuppliesDffRequestValidation()
    {
        RuleFor(x => x.SoLuongKemPhamChat).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.SoLuongMatPhamChat).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(o => o.SoLuongDong).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        RuleFor(x => x.SoLuongDeNghiThanhLy).GreaterThanOrEqualTo(0).WithMessage(MustGreaterThanOrEqual);
        //RuleFor(x => x.TsKemPcMatPc).GreaterThanOrEqualTo(0);
    }
}