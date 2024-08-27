using System.Text.Json;
using System.Text.Json.Serialization;

namespace ESPlatform.QRCode.IMS.Library.Utils.Converters;

public class JsonNullableConverter<T> : JsonConverter<T?> where T : struct {
	public JsonNullableConverter(JsonSerializerOptions options) { }

	public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
		if (typeToConvert != typeof(string)
		 && reader.TokenType == JsonTokenType.String
		 && string.IsNullOrWhiteSpace(reader.GetString())) {
			return null;
		}

		return JsonSerializer.Deserialize<T>(ref reader, options);
	}

	public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options) {
		if (value != null) {
			JsonSerializer.Serialize(writer, value.Value, options);
		}
	}
}
