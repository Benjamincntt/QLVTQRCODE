using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;

namespace ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;

public interface INguoiDungService
{
    Task<int> ModifyAsync(int maNguoiDung, ModifiedUserRequest request);

    Task<int> ResetPasswordAsync(int maNguoiDung, string password);

    Task<int> UpdatePassWordAsync(ModifiedUserPasswordRequest request);
    
    Task<int> CreateAsync(CreatedUserRequest userRequest);
}