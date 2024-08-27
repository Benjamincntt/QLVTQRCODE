using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ESPlatform.QRCode.IMS.Domain.Enums;
using ESPlatform.QRCode.IMS.Domain.ValueObjects;
using ESPlatform.QRCode.IMS.Library.Utils.Mappers;
using Mapster;

namespace ESPlatform.QRCode.IMS.Domain.Entities;

[DisplayName("tài khoản")]
public class Account {
	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	public Guid AccountId { get; set; }

	public string Username { get; set; } = null!;

	[JsonIgnore]
	public string Password { get; set; } = null!;

	public string FullName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string PhoneNumber { get; set; } = null!;

	public string OtpSecret { get; set; } = null!;

	public Guid AvatarFileId { get; set; }

	public string Title { get; set; } = null!;

	public Guid DepartmentLabelId { get; set; }

	public Guid GroupLabelId { get; set; }

	[Column(TypeName = "jsonb")]
	public AccountInfo Info { get; set; } = null!;

	public DateTime LoginFailedObservationTime { get; set; }

	public int LoginFailedCount { get; set; }

	public DateTime CreatedTime { get; set; }

	public DateTime ModifiedTime { get; set; }

	public DateTime LastLoginTime { get; set; }

	public DateTime LastActiveTime { get; set; }

	public bool IsLocked { get; set; }

	public DateTime LockedTime { get; set; }

	public AccountStatus Status { get; set; }

	[JsonIgnore, SimpleMapperIgnore, AdaptIgnore]
	public DateTime Timestamp { get; set; }
}
