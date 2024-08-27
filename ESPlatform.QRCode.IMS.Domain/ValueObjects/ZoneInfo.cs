using ESPlatform.QRCode.IMS.Domain.Enums;

namespace ESPlatform.QRCode.IMS.Domain.ValueObjects;

public class ZoneInfo : IEqualityComparer<ZoneInfo> {
	public Guid ZoneId { get; set; }

	public ZoneType ZoneType { get; set; }

	public bool Equals(ZoneInfo? x, ZoneInfo? y) {
		if (ReferenceEquals(x, y)) {
			return true;
		}

		if (x is null || y is null) {
			return false;
		}

		return x.ZoneId.Equals(y.ZoneId) && x.ZoneType == y.ZoneType;
	}

	public int GetHashCode(ZoneInfo obj) {
		return HashCode.Combine(obj.ZoneId, (int)obj.ZoneType);
	}
}
