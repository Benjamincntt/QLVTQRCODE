using ESPlatform.QRCode.IMS.Core.DTOs.KiemKe.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class SupplyListRequestValidation: AbstractValidator<SupplyListRequest>
{
    public SupplyListRequestValidation()
    {
        RuleFor(x => x.PageIndex).GreaterThanOrEqualTo(0);
        RuleFor(o => o.PageSize).GreaterThanOrEqualTo(0);
        RuleFor(o => o.IdViTri).GreaterThanOrEqualTo(0);
        RuleFor(o => o.IdKho).GreaterThanOrEqualTo(0);
        RuleFor(x => x.TenVatTu).ValidateStringDefault();
        RuleFor(x => x.MaVatTu).ValidateStringDefault();
    }
}