using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using FluentValidation;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class ModifiedSuppliesDffRequestValidation: AbstractValidator<ModifiedSuppliesDffRequest>
{
    public ModifiedSuppliesDffRequestValidation()
    {
        RuleFor(x => x.SoLuongKemPhamChat).GreaterThanOrEqualTo(0);
        RuleFor(o => o.SoLuongMatPhamChat).GreaterThanOrEqualTo(0);
        RuleFor(o => o.SoLuongDong).GreaterThanOrEqualTo(0);
        RuleFor(x => x.SoLuongDeNghiThanhLy).GreaterThanOrEqualTo(0);
        //RuleFor(x => x.TsKemPcMatPc).GreaterThanOrEqualTo(0);
    }
}