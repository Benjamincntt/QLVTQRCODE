using ESPlatform.QRCode.IMS.Api.Controllers.Base;
using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
using ESPlatform.QRCode.IMS.Core.DTOs.NguoiDungs.Requests;
using ESPlatform.QRCode.IMS.Core.Services.TbNguoiDungs;
using Microsoft.AspNetCore.Mvc;

namespace ESPlatform.QRCode.IMS.Api.Controllers;

public class NguoiDungsController : ApiControllerBase {
	private readonly INguoiDungService _nguoiDungService;

	public NguoiDungsController(INguoiDungService nguoiDungService) {
		_nguoiDungService = nguoiDungService;
	}
	
	/// <summary>Thêm mới tài khoản</summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost]
	public async Task<int> CreateAsync(NguoiDungCreatedRequest request) {
		return await _nguoiDungService.CreateAsync(request);
	}

	/// <summary>
	/// Cập nhật thông tin
	/// </summary>
	/// <param name="maNguoiDung"></param>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPatch("{maNguoiDung:int}")]
	public async Task<int> ModifyAsync(int maNguoiDung, [FromBody] NguoiDungModifyRequest request) {
		return await _nguoiDungService.ModifyAsync(maNguoiDung, request);
	}

	/// <summary>
	/// Quên mật khẩu
	/// </summary>
	/// <param name="maNguoiDung"></param>
	/// <param name="password"></param>
	/// <returns></returns>
	// [HttpPatch("{maNguoiDung:int}/reset-password")]
	// public async Task<int> ResetPasswordAsync(int maNguoiDung, [FromBody] string password) {
	// 	if (string.IsNullOrEmpty("password") || maNguoiDung == 0) {
	// 		return default;
	// 	}
	//
	// 	return await _nguoiDungService.ResetPasswordAsync(maNguoiDung, password);
	// }

	/// <summary>
	/// Đổi mật khẩu
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPatch]
	public async Task<int> UpdatePassWordAsync([FromBody] AccountUpdatePasswordRequest request) {
		return await _nguoiDungService.UpdatePassWordAsync(request);
	}
}