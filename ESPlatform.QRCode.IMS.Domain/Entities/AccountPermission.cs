using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Library.Utils.Mappers;
using Mapster;

namespace ESPlatform.QRCode.IMS.Domain.Entities;

public class AccountPermission {
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Guid AccountId { get; set; }

	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Guid PermissionId { get; set; }

	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Guid ZoneId { get; set; }

	public ZoneType ZoneType { get; set; }

	public AccountPermissionAction Action { get; set; }

	[JsonIgnore, SimpleMapperIgnore, AdaptIgnore]
	public DateTime Timestamp { get; set; }
}
