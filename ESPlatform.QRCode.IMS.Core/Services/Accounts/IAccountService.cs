using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;

namespace ESPlatform.QRCode.IMS.Core.Services.Accounts;

public interface IAccountService {
	//Task<int> CreateAsync(AccountCreatedRequest model);

	//Task<AccountResponse> GetAsync(Guid accountId);

	//Task<int> ModifyAsync(Guid accountId, AccountModifyRequest request);

	Task<Guid> ResetPasswordAsync(Guid accountId, string password);

	//Task<Guid> ResetOtpSecretAsync(Guid accountId);

	//Task<PagedList<AccountListResponseItem>> ListPagedAsync(AccountListRequest request);

	Task<int> UpdatePassWordAsync(ModifiedUserPasswordRequest request);

	//Task<IEnumerable<AccountAuthorizedResponseItem>> GetAuthorizedAccountsAsync(Guid categoryId, Guid roleId);

	//Task<IEnumerable<AccountListByCurrentSiteResponseItem>> ListByCurrentSiteAsync(Guid currentSiteId);
}
