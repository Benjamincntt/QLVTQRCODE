using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace ESPlatform.QRCode.IMS.Core.Facades.Context;

public class AuthorizedContextFacade : IAuthorizedContextFacade {
	public AuthorizedContextFacade(IHttpContextAccessor httpContextAccessor) {
		var httpContext = httpContextAccessor.HttpContext;

		// OBSOLETED
		// SiteId = httpContext?.Items[Constants.ContextKeys.SiteId] as Guid? ?? Guid.Empty;

		AccountId = httpContext?.Items[Constants.ContextKeys.AccountId] as int? ?? 0;
		Username = httpContext?.Items[Constants.ContextKeys.Username] as string ?? string.Empty;
		IpAddress = httpContext?.Items[Constants.ContextKeys.IpAddress] as string ?? string.Empty;

		RoleDictionary = httpContext?.Items[Constants.ContextKeys.RoleDictionary] as Dictionary<Guid, HashSet<ZoneInfo>> ?? new();
		PermissionDictionary = httpContext?.Items[Constants.ContextKeys.PermissionDictionary] as Dictionary<Guid, HashSet<ZoneInfo>> ?? new();
	}

	// OBSOLETED
	// public Guid SiteId { get; set; }

	public int AccountId { get; set; }

	public string Username { get; set; }

	public string IpAddress { get; set; }

	public Dictionary<Guid, HashSet<ZoneInfo>> RoleDictionary { get; set; }

	public Dictionary<Guid, HashSet<ZoneInfo>> PermissionDictionary { get; set; }

	public bool IsRoleGranted(Guid roleId) {
		return RoleDictionary.ContainsKey(roleId);
	}

	public bool IsAnyOfRolesGranted(IEnumerable<Guid> roleIds) {
		return roleIds.Any(roleId => RoleDictionary.ContainsKey(roleId));
	}

	public bool IsRoleGrantedOnZones(Guid roleId, IEnumerable<ZoneInfo> zones) {
		if (!RoleDictionary.ContainsKey(roleId)) {
			return false;
		}

		return RoleDictionary.TryGetValue(roleId, out var zoneSet) && zones.Any(zone => zoneSet.Contains(zone));
	}

	public bool IsAnyOfRolesGrantedOnZone(IEnumerable<Guid> roleIds, Guid zoneId, ZoneType type) {
		return ListZonesHavingAnyOfRolesGrantedOn(roleIds, type).Select(x => x.ZoneId).Contains(zoneId);
	}

	public bool IsAnyOfRolesGrantedOnZones(IEnumerable<Guid> roleIds, IEnumerable<Guid> zoneIds, ZoneType type) {
		var eligibleZones = ListZonesHavingAnyOfRolesGrantedOn(roleIds, type).Select(x => x.ZoneId);
		return !zoneIds.Except(eligibleZones).Any();
	}

	public bool IsPermissionGranted(Guid permissionId) {
		return PermissionDictionary.ContainsKey(permissionId);
	}

	public bool IsAnyOfPermissionsGranted(IEnumerable<Guid> permissionIds) {
		return permissionIds.Any(permissionId => PermissionDictionary.ContainsKey(permissionId));
	}

	public bool IsPermissionGrantedOnZones(Guid permissionId, IEnumerable<ZoneInfo> zones) {
		if (!PermissionDictionary.ContainsKey(permissionId)) {
			return false;
		}

		return PermissionDictionary.TryGetValue(permissionId, out var zoneSet) && zones.Any(zone => zoneSet.Contains(zone));
	}

	public IEnumerable<ZoneInfo> ListZonesHavingAnyOfRolesGrantedOn(IEnumerable<Guid> roleIds, ZoneType type) {
		return roleIds
			  .SelectMany(roleId => RoleDictionary.TryGetValue(roleId, out var zoneSet)
							  ? zoneSet.Where(zone => zone.ZoneType == type)
							  : Enumerable.Empty<ZoneInfo>())
			  .Distinct();
	}
}
