using System.ComponentModel;
using System.Reflection;

namespace ESPlatform.QRCode.IMS.Library.Utils;

public class DisplayNameResolver {
	private static readonly Dictionary<string, string> DisplayNameCache = new();

	public static string GetDisplayName(Type type, MemberInfo member) {
		var key = $"{type.FullName}_{member.Name}";

		// nếu tồn tại cache => display name
		var displayName = DisplayNameCache.GetValueOrDefault(key, string.Empty);

		if (!string.IsNullOrEmpty(displayName)) {
			return displayName;
		}

		// nếu không tồn tại cache
		var displayNameAttribute = member.GetCustomAttribute<DisplayNameAttribute>();
		displayName = displayNameAttribute != null ? displayNameAttribute.DisplayName : member.Name;

		DisplayNameCache.Add(key, displayName);
		return displayName;
	}
}
