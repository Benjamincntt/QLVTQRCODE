using System.Text.Json;
using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using ESPlatform.QRCode.IMS.Domain.Entities;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace ESPlatform.QRCode.IMS.Api.Middlewares;

public class AuthenticationMiddleware {
	private readonly RequestDelegate _next;

	public AuthenticationMiddleware(RequestDelegate next) {
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context,
								  IDistributedCache distributedCache) {
		var currentSiteId = context.GetSiteId();

		var endpoint = context.GetEndpoint();
		if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() == null && context.User.Identity?.IsAuthenticated != true) {
			throw new BadRequestException(Constants.Exceptions.Messages.Common.Unauthorized);
		}

		var currentAccountId = context.GetAccountId();

		context.Items[Constants.ContextKeys.SiteId] = currentSiteId;
		context.Items[Constants.ContextKeys.IpAddress] = context.GetClientIpAddress();
		context.Items[Constants.ContextKeys.AccountId] = currentAccountId;
		context.Items[Constants.ContextKeys.Username] = context.GetUsername();

		// Build Role Dictionary
		//var cacheKey = string.Format(Constants.CacheKeys.RoleDictionaryFormat, currentAccountId);
		// var cacheKey = currentAccountId.ToString();
		//
		// var cacheValue = await distributedCache.GetStringAsync(cacheKey.ToString());
		// if (cacheValue != null) {
		// 	var roleDict = JsonSerializer.Deserialize<Dictionary<Guid, HashSet<ZoneInfo>>>(cacheValue);
		// 	context.Items[Constants.ContextKeys.RoleDictionary] = roleDict;
		// }
		// else {
		// 	var accountRoles = (await accountRoleRepository.ListAsync(x => x.AccountId == currentAccountId)).ToList();
		// 	var roleDict = BuildRoleDictionary(accountRoles);
		//
		// 	context.Items[Constants.ContextKeys.RoleDictionary] = roleDict;
		// 	cacheValue = JsonSerializer.Serialize(roleDict);
		//
		// 	await distributedCache.SetStringAsync(cacheKey, cacheValue);
		// }
		//await distributedCache.SetStringAsync(cacheKey.ToString(), cacheValue);

		// Build Permission Dictionary
		// cacheKey = string.Format(Constants.CacheKeys.PermissionDictionaryFormat, currentAccountId);
		//
		// cacheValue = await distributedCache.GetStringAsync(cacheKey);
		// if (cacheValue != null) {
		// 	var permissionDict = JsonSerializer.Deserialize<Dictionary<Guid, HashSet<ZoneInfo>>>(cacheValue);
		// 	context.Items[Constants.ContextKeys.PermissionDictionary] = permissionDict;
		// }
		// else {
		// 	var allowedAccountPermissions = new List<AccountPermission>();
		// 	allowedAccountPermissions.AddRange(await accountPermissionRepository.ListByAccountRolesAsync(currentSiteId, currentAccountId));
		// 	allowedAccountPermissions.AddRange(await accountPermissionRepository.ListAsync(x => x.AccountId == currentAccountId
		// 																					 && x.Action == AccountPermissionAction.Allow));
		//
		// 	var blockedAccountPermissions = new List<AccountPermission>();
		// 	blockedAccountPermissions.AddRange(await accountPermissionRepository.ListAsync(x => x.AccountId == currentAccountId
		// 																					 && x.Action == AccountPermissionAction.Block));
		//
		// 	var permissionDict = BuildPermissionDictionary(allowedAccountPermissions, blockedAccountPermissions);
		//
		// 	context.Items[Constants.ContextKeys.PermissionDictionary] = permissionDict;
		// 	cacheValue = JsonSerializer.Serialize(permissionDict);
		//
		// 	await distributedCache.SetStringAsync(cacheKey, cacheValue);
		// }

		// Awaiting next sequence
		await _next(context);
	}

	// private static Dictionary<Guid, HashSet<ZoneInfo>> BuildRoleDictionary(List<AccountRole> accountRoles) {
	// 	var roleDictionary = new Dictionary<Guid, HashSet<ZoneInfo>>();
	// 	foreach (var accountRole in accountRoles) {
	// 		var zoneInfo = new ZoneInfo { ZoneId = accountRole.ZoneId, ZoneType = accountRole.ZoneType };
	//
	// 		if (roleDictionary.TryGetValue(accountRole.RoleId, out var zoneSet)) {
	// 			zoneSet.Add(zoneInfo);
	// 			continue;
	// 		}
	//
	// 		zoneSet = new HashSet<ZoneInfo> { zoneInfo };
	// 		roleDictionary.Add(accountRole.RoleId, zoneSet);
	// 	}
	//
	// 	return roleDictionary;
	// }

	private static Dictionary<Guid, HashSet<ZoneInfo>> BuildPermissionDictionary(List<AccountPermission> allowedAccountPermissions,
																				 List<AccountPermission> blockedAccountPermissions) {
		var permissionDictionary = new Dictionary<Guid, HashSet<ZoneInfo>>();
		foreach (var accountPermission in allowedAccountPermissions) {
			var zoneInfo = new ZoneInfo { ZoneId = accountPermission.ZoneId, ZoneType = accountPermission.ZoneType };

			if (permissionDictionary.TryGetValue(accountPermission.PermissionId, out var zoneSet)) {
				zoneSet.Add(zoneInfo);
				continue;
			}

			zoneSet = new HashSet<ZoneInfo> { zoneInfo };
			permissionDictionary.Add(accountPermission.PermissionId, zoneSet);
		}

		foreach (var accountPermission in blockedAccountPermissions) {
			var zoneInfo = new ZoneInfo { ZoneId = accountPermission.ZoneId, ZoneType = accountPermission.ZoneType };

			if (!permissionDictionary.TryGetValue(accountPermission.PermissionId, out var zoneSet)) {
				continue;
			}

			zoneSet.Remove(zoneInfo);
			if (zoneSet.Count == 0) {
				permissionDictionary.Remove(accountPermission.PermissionId);
			}
		}

		return permissionDictionary;
	}
}
