// #if DEBUG
// using ESPlatform.QRCode.IMS.Api.Controllers.Base;
// using ESPlatform.QRCode.IMS.Domain.Entities;
// using ESPlatform.QRCode.IMS.Infra.Context;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
//
// namespace ESPlatform.QRCode.IMS.Api.Controllers;
//
// [Route("/api/v1/utils")]
// public class ZzUtilsController : ApiControllerBase {
// 	private const string Key = "234j3k41a";
//
// 	private AppDbContext DbContext { get; }
//
// 	public ZzUtilsController(AppDbContext dbContext) {
// 		DbContext = dbContext;
// 	}
//
// 	[HttpPost("add-roles")]
// 	public async Task<IActionResult> AddRolesAsync([FromHeader] string key, [FromQuery] string username, [FromQuery] string roleNames, [FromQuery] string catUrlSlugs) {
// 		if (key != Key) {
// 			return BadRequest();
// 		}
//
// 		var accountId = await DbContext.Accounts.Where(x => x.Username == username).Select(x => x.AccountId).SingleAsync();
//
// 		List<Guid> roleIds;
// 		if (roleNames != "*") {
// 			var roleNameList = roleNames.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
// 			roleIds = await DbContext.Roles.Where(x => roleNameList.Contains(x.Name.ToLower())).Select(x => x.RoleId).ToListAsync();
// 		}
// 		else {
// 			roleIds = await DbContext.Roles.Select(x => x.RoleId).ToListAsync();
// 		}
//
// 		List<Guid> catIds;
// 		if (catUrlSlugs != "*") {
// 			var catUrlSlugList = catUrlSlugs.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
// 			catIds = await DbContext.Categories.Where(x => catUrlSlugList.Contains(x.UrlSlug.ToLower())).Select(x => x.CategoryId).ToListAsync();
// 		}
// 		else {
// 			catIds = await DbContext.Categories.Select(x => x.CategoryId).ToListAsync();
// 		}
//
// 		var accountRoles = (from roleId in roleIds
// 							from catId in catIds
// 							select new AccountRole { AccountId = accountId, RoleId = roleId, ZoneId = catId, ZoneType = ZoneType.Category })
// 		   .ToList();
//
// 		var result = await DbContext.AccountRoles.UpsertRange(accountRoles).RunAsync();
//
// 		return Ok(result);
// 	}
//
// 	[HttpDelete("remove-roles")]
// 	public async Task<IActionResult> RemoveRolesAsync([FromHeader] string key, [FromQuery] string username, [FromQuery] string roleNames, [FromQuery] string catUrlSlugs) {
// 		if (key != Key) {
// 			return BadRequest();
// 		}
//
// 		var accountId = await DbContext.Accounts.Where(x => x.Username == username).Select(x => x.AccountId).SingleAsync();
//
// 		List<Guid> roleIds;
// 		if (roleNames != "*") {
// 			var roleNameList = roleNames.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
// 			roleIds = await DbContext.Roles.Where(x => roleNameList.Contains(x.Name.ToLower())).Select(x => x.RoleId).ToListAsync();
// 		}
// 		else {
// 			roleIds = await DbContext.Roles.Select(x => x.RoleId).ToListAsync();
// 		}
//
// 		List<Guid> catIds;
// 		if (catUrlSlugs != "*") {
// 			var catUrlSlugList = catUrlSlugs.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);
// 			catIds = await DbContext.Categories.Where(x => catUrlSlugList.Contains(x.UrlSlug.ToLower())).Select(x => x.CategoryId).ToListAsync();
// 		}
// 		else {
// 			catIds = await DbContext.Categories.Select(x => x.CategoryId).ToListAsync();
// 		}
//
// 		var accountRoles = (from roleId in roleIds
// 							from catId in catIds
// 							select new AccountRole { AccountId = accountId, RoleId = roleId, ZoneId = catId, ZoneType = ZoneType.Category })
// 		   .ToList();
//
// 		DbContext.AccountRoles.RemoveRange(accountRoles);
// 		var result = await DbContext.SaveChangesAsync();
//
// 		return Ok(result);
// 	}
// }
// #endif
