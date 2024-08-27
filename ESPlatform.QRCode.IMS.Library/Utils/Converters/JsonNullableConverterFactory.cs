using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESPlatform.QRCode.IMS.Library.Utils.Converters;

public class JsonNullableConverterFactory : JsonConverterFactory {
	public override bool CanConvert(Type typeToConvert) {
		return Nullable.GetUnderlyingType(typeToConvert) != null;
	}

	public override JsonConverter? CreateConverter(Type type, JsonSerializerOptions options) {
		var underlyingType = Nullable.GetUnderlyingType(type);
		if (underlyingType == null) {
			return default;
		}

		return (JsonConverter?)Activator.CreateInstance(
			typeof(JsonNullableConverter<>).MakeGenericType(underlyingType),
			BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: new object[] { options },
			culture: null
		);
	}
}
