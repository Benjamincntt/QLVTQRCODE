using System.ComponentModel;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class ObjectExtensions {
	private static readonly JsonSerializerOptions JsonOption = new() {
		Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		WriteIndented = false
	};

	private static readonly Type TypeOfString = typeof(string);

	public static string ToJson(this object? obj) {
		return obj != null ? JsonSerializer.Serialize(obj, JsonOption) : "{}";
	}

	public static bool IsSimpleType(this object? obj) {
		return obj == null || TypeDescriptor.GetConverter(obj).CanConvertTo(TypeOfString);
	}

	public static object? GetPropertyValueFromPath(this object? obj, string propertyPath) {
		var propNames = propertyPath.Split('.');

		if (obj == null) {
			return null;
		}

		var currentObj = obj;
		var currentType = currentObj.GetType();
		foreach (var propName in propNames) {
			var propInfo = currentType.GetProperty(propName);
			if (propInfo == null) {
				return null;
			}

			currentObj = propInfo.GetValue(currentObj, null);
			currentType = propInfo.PropertyType;
		}

		return currentObj;
	}
}
