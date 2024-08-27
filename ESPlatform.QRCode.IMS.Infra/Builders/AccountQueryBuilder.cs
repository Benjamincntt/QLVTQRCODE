using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Infra.Builders;

public interface IAccountQueryBuilder {
	AccountQueryBuilder WithKeyword(string? keyword);

	// AccountQueryBuilder WithStatus(AccountStatus status);
}

public class AccountQueryBuilder : IAccountQueryBuilder {
	private IQueryable<Account> _query;

	public AccountQueryBuilder(IQueryable<Account> query) {
		_query = query;
	}

	public AccountQueryBuilder WithKeyword(string? keyword) {
		if (!string.IsNullOrEmpty(keyword)) {
			_query = _query.Where(o => o.Username.Contains(keyword) || o.Email.Contains(keyword) || o.FullName.Contains(keyword));
		}

		return this;
	}

	// public AccountQueryBuilder WithStatus(AccountStatus status) {
	// 	if (status != AccountStatus.Unknown) {
	// 		_query = _query.Where(a => a.Status == status);
	// 	}
	//
	// 	return this;
	// }

	public IQueryable<Account> Build() {
		return _query;
	}
}
