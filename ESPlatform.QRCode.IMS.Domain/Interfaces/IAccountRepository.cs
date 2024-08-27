using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Models.Accounts;
using ESPlatform.QRCode.IMS.Library.Database;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;

namespace ESPlatform.QRCode.IMS.Domain.Interfaces;

public interface IAccountRepository : IRepositoryBase<Account> {
	// Task<dynamic?> GetDetailAsync(Guid accountId);

	// Task<PagedList<Account>> ListPagedAsync(AccountListRequest request);
	//
	// Task<IEnumerable<dynamic>> GetAuthorizedAccountsAsync(Guid categoryId, Guid roleId);
	//
	// Task<IEnumerable<dynamic>> ListByCurrentSiteAsync(Guid currentSiteId);
}
