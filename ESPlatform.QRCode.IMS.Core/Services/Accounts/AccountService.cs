using ESPlatform.QRCode.IMS.Core.DTOs.Accounts.Requests;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Facades.Context;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using MapsterMapper;

namespace ESPlatform.QRCode.IMS.Core.Services.Accounts;

public class AccountService : IAccountService {
	private readonly IAccountRepository _accountRepository;
	//private readonly ILogFacade _logFacade;
	private readonly IAuthorizedContextFacade _authorizedContextFacade;
	private readonly IMapper _mapper;

	public AccountService(IAccountRepository accountRepository,
		//ILogFacade logFacade,
		IAuthorizedContextFacade authorizedContextFacade,
		IMapper mapper) {
		_accountRepository = accountRepository;
		//_logFacade = logFacade;
		_authorizedContextFacade = authorizedContextFacade;
		_mapper = mapper;
	}

	// public async Task<int> CreateAsync(AccountCreatedRequest request) {
	// 	await ValidationHelper.ValidateAsync(request, new AccountInsertModelValidation());
	// 	var currentAccount = await _accountRepository.GetAsync(a => a.Username == request.Username);
	// 	if (currentAccount != null) {
	// 		throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
	// 	}
	// 	var account = request.Adapt<Account>();
	// 	account.AccountId = Guid.NewGuid();
	// 	account.Status = AccountStatus.Registered;
	// 	account.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
	// 	//account.OtpSecret = OtpFacade.GenerateSecretKey();
	// 	account.CreatedTime = DateTime.UtcNow;
	// 	return await _accountRepository.InsertAsync(account);
	// }

	// public async Task<AccountResponse> GetAsync(Guid accountId) {
	// 	if (accountId == Guid.Empty) {
	// 		throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidParameters);
	// 	}
	//
	// 	var data = await _accountRepository.GetDetailAsync(accountId);
	// 	if (data == null) {
	// 		throw new NotFoundException( "Tài khoản", accountId.ToString());
	// 	}
	//
	// 	var response = _mapper.Map<AccountResponse>(data);
	// 	return response;
	// }

	// public async Task<int> ModifyAsync(Guid accountId, AccountModifyRequest request) {
	// 	var account = await _accountRepository.GetAsync(accountId);
	// 	if (account == null) {
	// 		throw new NotFoundException(account.GetTypeEx(), accountId.ToString());
	// 	}
	//
	// 	await ValidationHelper.ValidateAsync(request, new AccountModifyRequestValidation());
	// 	if (request.Status == AccountStatus.Registered) {
	// 		throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidStatus);
	// 	}
	//
	// 	account = request.Adapt(account);
	//
	// 	var response = await _accountRepository.UpdateAsync(account);
	// 	await _logFacade.LogAsync(Constants.Logging.AccountModifyLogAction, accountId, JsonSerializer.Serialize(request));
	//
	// 	return response;
	// }

	public async Task<Guid> ResetPasswordAsync(Guid accountId, string password) {
		var account = await _accountRepository.GetAsync(accountId);
		if (account == null) {
			throw new NotFoundException(account.GetTypeEx(), accountId.ToString());
		}

		account.Password = BCrypt.Net.BCrypt.HashPassword(password);
		await _accountRepository.UpdateAsync(account);
		return accountId;
	}

	// public async Task<Guid> ResetOtpSecretAsync(Guid accountId) {
	// 	var account = await _accountRepository.GetAsync(accountId);
	// 	if (account == null) {
	// 		throw new NotFoundException(account.GetTypeEx(), accountId.ToString());
	// 	}
	//
	// 	account.OtpSecret = OtpFacade.GenerateSecretKey();
	// 	account.Status = AccountStatus.Unknown;
	// 	await _accountRepository.UpdateAsync(account);
	// 	await _logFacade.LogAsync(Constants.Logging.AccountModifyLogAction, accountId, JsonSerializer.Serialize(account.OtpSecret));
	// 	return accountId;
	// }

	// public async Task<PagedList<AccountListResponseItem>> ListPagedAsync(AccountListRequest request) {
	// 	await ValidationHelper.ValidateAsync(request, new AccountListRequestValidation());
	// 	var data = await _accountRepository.ListPagedAsync(request);
	// 	var response = data.Adapt<PagedList<AccountListResponseItem>>();
	// 	return response;
	// }

	public async Task<int> UpdatePassWordAsync(ModifiedUserPasswordRequest request) {
		var accountId = _authorizedContextFacade.AccountId;
		var account = await _accountRepository.GetAsync(accountId);
		if (account == null) {
			throw new NotFoundException(account.GetTypeEx(), accountId.ToString());
		}

		if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, account.Password)) {
			throw new BadRequestException(Constants.Exceptions.Messages.Common.InvalidPassword);
		}

		account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
		return await _accountRepository.UpdateAsync(account);
	}

	// public async Task<IEnumerable<AccountAuthorizedResponseItem>> GetAuthorizedAccountsAsync(Guid categoryId, Guid roleId) {
	// 	var listAccount = await _accountRepository.GetAuthorizedAccountsAsync(categoryId, roleId);
	// 	return listAccount.Adapt<IEnumerable<AccountAuthorizedResponseItem>>().ToList();
	// }
	//
	// public async Task<IEnumerable<AccountListByCurrentSiteResponseItem>> ListByCurrentSiteAsync(Guid currentSiteId) {
	// 	return (await _accountRepository.ListByCurrentSiteAsync(currentSiteId))
	// 		.Adapt<IEnumerable<AccountListByCurrentSiteResponseItem>>();
	// }
}
