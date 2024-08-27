using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.ValueObjects;

namespace ESPlatform.QRCode.IMS.Core.Facades.Context;

public interface IAuthorizedContextFacade {
	// Guid SiteId { get; set; }

	int AccountId { get; set; }

	string Username { get; set; }

	string IpAddress { get; set; }

	Dictionary<Guid, HashSet<ZoneInfo>> RoleDictionary { get; set; }

	Dictionary<Guid, HashSet<ZoneInfo>> PermissionDictionary { get; set; }

	bool IsRoleGranted(Guid roleId);

	bool IsAnyOfRolesGranted(IEnumerable<Guid> roleIds);

	bool IsRoleGrantedOnZones(Guid roleId, IEnumerable<ZoneInfo> zones);

	bool IsAnyOfRolesGrantedOnZone(IEnumerable<Guid> roleIds, Guid zoneId, ZoneType type);

	bool IsAnyOfRolesGrantedOnZones(IEnumerable<Guid> roleIds, IEnumerable<Guid> zoneIds, ZoneType type);

	bool IsPermissionGranted(Guid permissionId);

	bool IsAnyOfPermissionsGranted(IEnumerable<Guid> permissionIds);

	bool IsPermissionGrantedOnZones(Guid permissionId, IEnumerable<ZoneInfo> zones);

	IEnumerable<ZoneInfo> ListZonesHavingAnyOfRolesGrantedOn(IEnumerable<Guid> roleIds, ZoneType type);
}
