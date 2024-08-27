using System.ComponentModel;
using System.Reflection;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class MiscExtensions {
	public static Type GetTypeEx<T>(this T t) {
		return t != null ? t.GetType() : typeof(T);
	}

	public static string GetTypeName<T>(this T t) {
		return GetTypeEx(t).Name;
	}

	public static string GetDisplayName(this Type type) {
		var displayNameAttribute = type.GetCustomAttribute<DisplayNameAttribute>();
		return displayNameAttribute?.DisplayName ?? type.Name;
	}
}
