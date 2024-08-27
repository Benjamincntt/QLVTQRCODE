using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.Interfaces;
using ESPlatform.QRCode.IMS.Domain.Models.Accounts;
using ESPlatform.QRCode.IMS.Infra.Builders;
using ESPlatform.QRCode.IMS.Infra.Context;
using ESPlatform.QRCode.IMS.Library.Database.EfCore;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Infra.Repositories;

public class AccountRepository : EfCoreRepositoryBase<Account, AppDbContext>, IAccountRepository {
	public AccountRepository(AppDbContext dbContext) : base(dbContext) { }

	// public async Task<dynamic?> GetDetailAsync(Guid accountId) {
	// 	return await DbContext.Accounts
	// 		.GroupJoin(DbContext.Files,
	// 			x => x.AvatarFileId,
	// 			y => y.FileId,
	// 			(x, y) => new { Account = x, Files = y })
	// 		.SelectMany(x => x.Files.DefaultIfEmpty(),
	// 			(x, y) => new { x.Account, AvatarFile = y })
	// 		.GroupJoin(DbContext.Labels,
	// 			x => x.Account.DepartmentLabelId,
	// 			y => y.LabelId,
	// 			(x, y) => new { x.Account, x.AvatarFile, DepartmentLabel = y })
	// 		.SelectMany(x => x.DepartmentLabel.DefaultIfEmpty(),
	// 			(x, y) => new { x.Account, x.AvatarFile, DepartmentLabel = y })
	// 		.GroupJoin(DbContext.Labels,
	// 			x => x.Account.GroupLabelId,
	// 			y => y.LabelId,
	// 			(x, y) => new { x.Account, x.AvatarFile, x.DepartmentLabel, GroupLabel = y })
	// 		.SelectMany(x => x.GroupLabel.DefaultIfEmpty(),
	// 			(x, y) => new { x.Account, x.AvatarFile, x.DepartmentLabel, GroupLabel  = y })
	// 		.Where(x => x.Account.AccountId == accountId)
	// 		.Select(x => new {
	// 			x.Account.Username,
	// 			x.Account.FullName,
	// 			x.Account.Email,
	// 			x.Account.PhoneNumber,
	// 			x.Account.AvatarFileId,
	// 			x.Account.IsLocked,
	// 			x.Account.Status,
	// 			x.Account.Title,
	// 			x.Account.DepartmentLabelId,
	// 			x.Account.GroupLabelId,
	// 			AvatarUrl = x.AvatarFile != null ? x.AvatarFile.FilePath : string.Empty,
	// 			DepartmentName = x.DepartmentLabel != null ? x.DepartmentLabel.Name : string.Empty,
	// 			GroupName = x.GroupLabel != null ? x.GroupLabel.Name : string.Empty
	// 		})
	// 		.FirstOrDefaultAsync();
	// }
	//
	// public async Task<PagedList<Account>> ListPagedAsync(AccountListRequest request) {
	// 	var query = new AccountQueryBuilder(DbContext.Accounts)
	// 		.WithKeyword(request.Keywords)
	// 		.WithStatus(request.Status)
	// 		.Build()
	// 		.OrderBy(o => o.Username);
	//
	// 	var response = await query
	// 		.ToPagedListAsync(request.GetPageIndex(), request.GetPageSize());
	//
	// 	return response;
	// }

	// public async Task<IEnumerable<dynamic>> GetAuthorizedAccountsAsync(Guid categoryId, Guid roleId) {
	// 	return await DbContext.Accounts
	// 		.Join(DbContext.AccountRoles,
	// 			x => x.AccountId,
	// 			y => y.AccountId,
	// 			(x, y) => new { Account = x, AccountRole = y })
	// 		.Where(x => x.AccountRole.RoleId == roleId)
	// 		.Where(x => x.Account.Status == AccountStatus.Activated)
	// 		.Where(x => x.AccountRole.ZoneId == categoryId && x.AccountRole.ZoneType == ZoneType.Category)
	// 		.Select(x => new {
	// 			x.Account.AccountId,
	// 			x.Account.Username,
	// 			x.Account.FullName
	// 		})
	// 		.OrderByDescending(x => x.FullName)
	// 		.ToListAsync();
	// }
	//
	// public async Task<IEnumerable<dynamic>> ListByCurrentSiteAsync(Guid currentSiteId) {
	// 	return await DbContext.Accounts
	// 		.Where(x => x.Status == AccountStatus.Activated)
	// 		.OrderBy(x => x.FullName)
	// 		.Select(x => new {
	// 			x.AccountId,
	// 			x.Username,
	// 			x.FullName
	// 		})
	// 		.ToListAsync();
	// }
}