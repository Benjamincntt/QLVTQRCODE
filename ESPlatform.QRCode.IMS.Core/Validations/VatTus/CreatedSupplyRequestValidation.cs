using ESPlatform.QRCode.IMS.Core.DTOs.MuaSamVatTu.Requests;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using FluentValidation;

namespace ESPlatform.QRCode.IMS.Core.Validations.VatTus;

public class CreatedSupplyRequestValidation: AbstractValidator<CreatedSupplyRequest>
{
    public CreatedSupplyRequestValidation()
    {
        RuleFor(x => x.MaVatTu).ValidateStringDefault(false);
        RuleFor(x => x.TenVatTu).ValidateStringDefault(false);
        RuleFor(x => x.DonViTinh).ValidateStringDefault(false);
        RuleFor(x => x.GhiChu).ValidateStringDefault();
        RuleFor(x => x.MoTa).ValidateStringDefault();
        
    }
}