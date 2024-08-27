// using ESPlatform.QRCode.IMS.Api.Controllers.Base;
// using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
// using ESPlatform.QRCode.IMS.Core.Services.Accounts;
//
// using Microsoft.AspNetCore.Mvc;
// namespace ESPlatform.QRCode.IMS.Api.Controllers;
//
// public class AccountsController : ApiControllerBase {
// 	private readonly IAccountService _accountService;
//
// 	public AccountsController(IAccountService accountService) {
// 		_accountService = accountService;
// 	}

	/// <summary>Api thêm mới tài khoản</summary>
	/// <param name="request"></param>
	/// <returns></returns>
	// [HttpPost]
	// public async Task<int> CreateAsync(AccountCreatedRequest request) {
	// 	return await _accountService.CreateAsync(request);
	// }

	/// <summary>Api lấy chi tiết tài khoản</summary>
	/// <param name="accountId">mã tài khoản</param>
	/// <returns></returns>
	// [HttpGet("{accountId:guid}")]
	// public async Task<AccountResponse> GetAsync(Guid accountId) {
	// 	return await _accountService.GetAsync(accountId);
	// }

	/// <summary>
	/// Api sửa tài khoản
	/// </summary>
	/// <param name="accountId"></param>
	/// <param name="request"></param>
	/// <returns></returns>
	// [HttpPatch("{accountId:guid}")]
	// public async Task<int> ModifyAsync(Guid accountId, AccountModifyRequest request) {
	// 	return await _accountService.ModifyAsync(accountId, request);
	// }

	/// <summary>Api reset mật khẩu người dùng</summary>
	/// <param name="accountId">mã tài khoản được sửa</param>
	/// <param name="password">mật khẩu</param>
	/// <returns>AccountUserId</returns>
	// [HttpPatch("{accountId:guid}/reset-password")]
	// public async Task<Guid> ResetPasswordAsync(Guid accountId, [FromBody] string password) {
	// 	if (string.IsNullOrEmpty("password") || accountId == Guid.Empty) {
	// 		return default;
	// 	}
	//
	// 	return await _accountService.ResetPasswordAsync(accountId, password); 
	// }

	/// <summary>Api reset key OTP cho người dùng</summary>
	/// <param name="accountId">mã tài khoản được sửa</param>
	/// <returns>accountUserId</returns>
	// [HttpPatch("{accountId:guid}/reset-otp")]
	// public async Task<Guid> ResetKeyOtpAsync(Guid accountId) {
	// 	if (accountId == Guid.Empty) {
	// 		return default;
	// 	}
	//
	// 	return await _accountService.ResetOtpSecretAsync(accountId);
	// }

	

	/// <summary>[CMS-78] Api đổi mật khẩu</summary>
	/// <param name="request"></param>
	/// <returns></returns>
	// [HttpPatch]
	// public async Task<int> UpdatePassWordAsync([FromBody]AccountUpdatePasswordRequest request) {
	// 	return await _accountService.UpdatePassWordAsync(request);
	// }

	/// <summary>
	/// [CMS-89] Api danh sách người dùng được phân quyền theo Role và chuyên mục
	/// </summary>
	/// <param name="categoryId"></param>
	/// <param name="roleId"></param>
	/// <returns></returns>
	// [HttpGet("list-authorized")]
	// public async Task<IEnumerable<AccountAuthorizedResponseItem>> GetAuthorizedAccountsAsync(Guid categoryId, Guid roleId) {
	// 	return await _accountService.GetAuthorizedAccountsAsync(categoryId, roleId);
	// }
// }
